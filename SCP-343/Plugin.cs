using System;
using Exiled.API.Features;
using Exiled.Events;
using Exiled.Events.EventArgs;
using Exiled.Events.Handlers;
using Exiled.API.Enums;
using PlayerEvent = Exiled.Events.Handlers.Player;
using MapEvent = Exiled.Events.Handlers.Map;
using WarheadEvent = Exiled.Events.Handlers.Warhead;
using ServerEvent = Exiled.Events.Handlers.Server;

namespace SCP_343
{
	internal class Plugin : Plugin<Config>
	{
		public override string Author { get; } = "An4r3w";

		public override string Name { get; } = "SCP-343";

		public override string Prefix { get; } = "SCP-343";

		public override Version Version { get; } = new Version(1, 1, 0);

		public override Version RequiredExiledVersion { get; } = new Version(2, 3, 4);

        internal EventHandlers EventHandlers { get; set; }

		public override void OnEnabled()
		{
			Plugin.Singleton = this;
			string[] array = new string[5];
			array[0] = this.Name;
			array[1] = " ";
			array[2] = this.Author;
			array[3] = " Version: ";
			int num = 4;
			Version version = this.Version;
			array[num] = ((version != null) ? version.ToString() : null);
			Log.Info(string.Concat(array));
			this.EventHandlers = new EventHandlers();
			PlayerEvent.ChangingRole += EventHandlers.onRoleChange;
			PlayerEvent.Died += EventHandlers.onDeath;
			PlayerEvent.Left += EventHandlers.onLeave;
			PlayerEvent.Handcuffing += EventHandlers.onDetain;
			PlayerEvent.DroppingItem += EventHandlers.onItemDrop;
			PlayerEvent.PickingUpItem += EventHandlers.onItemGrab;
			MapEvent.Decontaminating += EventHandlers.onDecontamination;
			WarheadEvent.Detonated += EventHandlers.onWarheadExplosion;
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
			MapEvent.Decontaminating -= EventHandlers.onDecontamination;
			WarheadEvent.Detonated -= EventHandlers.onWarheadExplosion;
			ServerEvent.RoundEnded -= EventHandlers.onRoundEnd;
			this.EventHandlers = null;
		}

		public override void OnReloaded()
		{
		}

		public static Plugin Singleton;
	}
}
