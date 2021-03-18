using System;
using Exiled.API.Features;
using PlayerEvent = Exiled.Events.Handlers.Player;
using ServerEvent = Exiled.Events.Handlers.Server;

namespace SCP_343
{
	internal class Plugin : Plugin<Config>
	{
		public override string Author { get; } = "An4r3w";

		public override string Name { get; } = "SCP-343";

		public override string Prefix { get; } = "SCP-343";

		public override Version Version { get; } = new Version(2, 0, 0);

		public override Version RequiredExiledVersion { get; } = new Version(2, 3, 3);

        internal EventHandlers EventHandlers { get; set; }

		public override void OnEnabled()
		{
			Plugin.Singleton = this;
			this.EventHandlers = new EventHandlers();
			PlayerEvent.ChangingRole += EventHandlers.onRoleChange;
			PlayerEvent.Died += EventHandlers.onDeath;
			PlayerEvent.Left += EventHandlers.onLeave;
			PlayerEvent.Handcuffing += EventHandlers.onDetain;
			PlayerEvent.DroppingItem += EventHandlers.onItemDrop;
			PlayerEvent.PickingUpItem += EventHandlers.onItemGrab;
			ServerEvent.RoundEnded += EventHandlers.onRoundEnd;
		}

		public override void OnDisabled()
		{
			PlayerEvent.ChangingRole -= EventHandlers.onRoleChange;
			PlayerEvent.Died -= EventHandlers.onDeath;
			PlayerEvent.Left -= EventHandlers.onLeave;
			PlayerEvent.Handcuffing -= EventHandlers.onDetain;
			PlayerEvent.DroppingItem -= EventHandlers.onItemDrop;
			PlayerEvent.PickingUpItem -= EventHandlers.onItemGrab;
			ServerEvent.RoundEnded -= EventHandlers.onRoundEnd;
			this.EventHandlers = null;
		}

		public static Plugin Singleton;
	}
}
