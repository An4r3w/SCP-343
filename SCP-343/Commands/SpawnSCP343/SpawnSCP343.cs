using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using MEC;
using SCP_343;
using RemoteAdmin;
namespace Commands.SpawnSCP343
{
	[CommandHandler(typeof(RemoteAdminCommandHandler))]
	internal class SpawnSCP343 : ICommand
	{

		public string Command { get; } = "spawnscp343";

		public string[] Aliases { get; } = new string[]
		{
			"343",
			"spawn343"
		};

		public string Description { get; } = "Spawn SCP-343!";

		public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
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
				if (player == null)
				{
					response = $"The player{Player.Get(player.Sender).Nickname} is invalid";
					return false;
				}
				Timing.CallDelayed(1f, delegate()
				{
					EventHandlers.spawn343(player);
				});
				response = "The player is now SCP-343";
				return true;
			}
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
			}
		}
	}
}
