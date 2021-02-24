using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using MEC;
using SCP_343;

namespace Commands.SpawnSCP343
{
	[CommandHandler(typeof(RemoteAdminCommandHandler))]
	[CommandHandler(typeof(GameConsoleCommandHandler))]
	internal class SpawnSCP343 : ParentCommand
	{
		public SpawnSCP343()
		{
			this.LoadGeneratedCommands();
		}

		public override string Command { get; } = "spawnscp343";

		public override string[] Aliases { get; } = new string[]
		{
			"343",
			"spawn343"
		};

		public override string Description { get; } = "Spawn SCP-343!";

		public override void LoadGeneratedCommands()
		{
		}

		protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
		{
			Player player = Player.Get(((CommandSender)sender).SenderId);
			if (!Permissions.CheckPermission((CommandSender)sender, "scp343.spawn"))
			{
				response = "You do not have permissions to run this command!";
				return false;
			}
			int count = arguments.Count;
			if (count != 0)
			{
				if (count != 1)
				{
					response = "Usage: spawn343";
					return false;
				}
				Player target = Player.Get(CollectionExtensions.At<string>(arguments, 0));
				if (target == null)
				{
					response = "The player " + CollectionExtensions.At<string>(arguments, 1) + " is invalid";
					return false;
				}
				target.SetRole((RoleType)14, false, false);
				Timing.CallDelayed(1f, delegate()
				{
					EventHandlers.spawn343(target);
				});
				response = "The player " + CollectionExtensions.At<string>(arguments, 1) + " is now SCP-343";
				return true;
			}
			else
			{
				if (EventHandlers.scp343.Count != 0)
				{
					response = "There is already a living SCP-343";
					return false;
				}
				player.SetRole((RoleType)1, false, false);
				Timing.CallDelayed(1f, delegate()
				{
					EventHandlers.spawn343(player);
				});
				response = "You spawned yourself as SCP-343";
				return true;

				// yo, if you found this ur a code stealer... or an Amathor too :)
			}
		}
	}
}
