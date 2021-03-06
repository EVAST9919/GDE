﻿using GDEdit.Application;
using GDEdit.Utilities.Objects.GeometryDash;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Testing;
using System.Threading.Tasks;

namespace GDE.Tests.Visual.TestSceneLevel
{
    public class TestSceneLoadLevel : TestScene
    {
        private Database database;
        private SpriteText levelName;
        private LevelCollection levels;
        private bool finishedLoading;

        public TestSceneLoadLevel()
        {
            Children = new Drawable[]
            {
                levelName = new SpriteText
                {
                    RelativeSizeAxes = Axes.Both,
                    AllowMultiline = true,
                    Font = new FontUsage(size: 40),
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load(DatabaseCollection d)
        {
            database = d[0];
        }

        protected override void Update()
        {
            if (!finishedLoading && (finishedLoading = database.DecryptLevelDataStatus >= TaskStatus.RanToCompletion))
                levelName.Text = (levels = database.UserLevels).Count > 0 ? $"Loaded: {levels[0].Name}" : "No levels";
            base.Update();
        }
    }
}
