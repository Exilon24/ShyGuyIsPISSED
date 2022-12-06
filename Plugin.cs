using Exiled.Events.EventArgs;
using HarmonyLib;

namespace ShyGuyIsPISSED
{
    using Exiled.API.Features;
    
    public class Plugin : Plugin<Config>
    {
        private Harmony harmony = new Harmony("Patch096");
        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Server.RoundEnded += EndRound;
            harmony.PatchAll();
        }

        private void EndRound(RoundEndedEventArgs ev)
        {
            harmony.UnpatchAll("Patch096");
        }
    }
}