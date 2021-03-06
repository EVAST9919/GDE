﻿using GDE.App.Main.Colors;
using GDE.App.Main.Containers;
using GDE.App.Main.Overlays;
using GDE.App.Main.Screens.Menu.Components;
using GDE.App.Main.Tools;
using GDE.App.Main.UI;
using GDE.App.Main.UI.Containers;
using GDEdit.Application;
using GDEdit.Utilities.Functions.Extensions;
using GDEdit.Utilities.Objects.GeometryDash;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Bindings;
using osu.Framework.Logging;
using osu.Framework.Screens;
using osuTK;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GDE.App.Main.Screens.Menu.Components
{
    public class LevelList : SearchContainer
    {
        private FillFlowContainer levelList;
        private Database database;
        private LevelCollection levels;
        private TextBox searchQuery;
        private bool finishedLoading;
        private bool alreadyRun;

        public int LevelIndex;
        public Action LevelSelected;
        public Action CompletedLoading;
        public List<LevelCard> Cards;

        public LevelList()
        {
            Children = new Drawable[]
            {
                searchQuery = new TextBox
                {
                    Size = new Vector2(230, 40),
                    Margin = new MarginPadding
                    {
                        Top = 5,
                        Left = 10
                    }
                },
                new GDEScrollContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Margin = new MarginPadding
                    {
                        Top = 5,
                    },
                    Children = new Drawable[]
                    {
                        levelList = new FillFlowContainer
                        {
                            LayoutDuration = 100,
                            LayoutEasing = Easing.Out,
                            Spacing = new Vector2(1, 1),
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Padding = new MarginPadding(5)
                        }
                    }
                },
            };

            Cards = new List<LevelCard>();
            searchQuery.Current.ValueChanged += obj =>
            {
                Console.WriteLine(obj.NewValue);
                foreach (var c in Cards)
                    if (obj.NewValue.MatchesSearchCriteria(c.Level.Value.LevelNameWithRevision))
                        c.Show();
                    else
                        c.Hide();
            };
        }

        [BackgroundDependencyLoader]
        private void load(DatabaseCollection databases)
        {
            database = databases[0];
        }

        protected override void Update()
        {
            if (!finishedLoading && (finishedLoading = database.GetLevelsStatus >= TaskStatus.RanToCompletion))
            {
                if ((levels = database.UserLevels).Count == 0)
                {
                    AddInternal(new FillFlowContainer
                    {
                        Direction = FillDirection.Vertical,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Spacing = new Vector2(0, 30),
                        Children = new Drawable[]
                        {
                            new SpriteIcon
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Icon = FontAwesome.Solid.Times,
                                Size = new Vector2(192),
                                Colour = GDEColors.FromHex("666666")
                            },
                            new SpriteText
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Text = "There doesn't seem to be anything here...",
                                Font = @"OpenSans",
                                TextSize = 24,
                                Colour = GDEColors.FromHex("666666")
                            },
                            new GDEButton
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Width = 200,
                                Text = "Create a new level",
                                BackgroundColour = GDEColors.FromHex("242424")
                            }
                        }
                    });
                }
                else if (!alreadyRun)
                {
                    for (var i = 0; i < database.UserLevels.Count; i++)
                    {
                        Cards.Add(new LevelCard
                        {
                            RelativeSizeAxes = Axes.X,
                            Size = new Vector2(0.9f, 60),
                            Margin = new MarginPadding(10),
                            Index = i,
                            Level =
                            {
                                Value = database.UserLevels[i]
                            }
                        });

                        Logger.Log($"Loaded: {database.UserLevels[i].LevelNameWithRevision}.");
                    }

                    foreach (var c in Cards)
                    {
                        levelList.Add(Cards[c.Index]);

                        Cards[c.Index].Selected.ValueChanged += obj =>
                        {
                            if (obj.NewValue)
                                foreach (var j in Cards)
                                    if (j != Cards[c.Index] && j.Selected.Value)
                                        j.Selected.Value = false;
                            LevelIndex = obj.NewValue ? c.Index : -1;
                            LevelSelected?.Invoke();
                        };
                    }

                    CompletedLoading?.Invoke();
                    alreadyRun = true;

                    Logger.Log("Loaded all levels successfully.");
                }
            }
        }
    }
}
