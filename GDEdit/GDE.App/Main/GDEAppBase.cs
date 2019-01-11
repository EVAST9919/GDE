﻿using osuTK;
using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using System.Drawing;
using osu.Framework.Development;
using System.Threading;
using osu.Framework.Logging;
using System.Threading.Tasks;

namespace GDE.App.Main
{
    public class GDEAppBase : Game
    {
        private const string mainResourceFile = "GDE.Resources.dll";

        private DependencyContainer dependencies;
        private Storage storage;

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        public GDEAppBase()
        {
            storage = new DesktopStorage("GD Edit", Host);
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            dependencies.Cache(this);
            dependencies.Cache(storage);

            Resources.AddStore(new DllResourceStore(mainResourceFile));

            //Fonts = new FontStore(new GlyphStore(Resources, @"Fonts/Arial"));
            //Fonts.AddStore(new GlyphStore(Resources, @"Fonts/FontAwesome"));

            //dependencies.Cache(Fonts);
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            var config = new FrameworkConfigManager(storage);

            config.Set(FrameworkSetting.WindowMode, WindowMode.Windowed);
            config.Set(FrameworkSetting.WindowedSize, new Size(1280, 720));

            Window.WindowBorder = WindowBorder.Fixed;
            Window.Title = @"GD Edit";

            host.ExceptionThrown += ExceptionHandler;

            Window.SetupWindow(config);
        }

        private static int allowableExceptions = DebugUtils.IsDebugBuild ? 0 : 1;

        private bool ExceptionHandler(System.Exception arg)
        {
            bool continueExecution = Interlocked.Decrement(ref allowableExceptions) >= 0;

            Logger.Log($"Unhandled exception has been {(continueExecution ? $"allowed with {allowableExceptions} more allowable exceptions" : "denied")} .");
            Task.Delay(1000).ContinueWith(_ => Interlocked.Increment(ref allowableExceptions));

            return continueExecution;
        }
    }
}
