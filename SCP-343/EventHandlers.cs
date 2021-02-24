using System;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using System.Linq;
using Exiled.API.Enums;

namespace SCP_343
{

	internal class EventHandlers
	{
		

		public void onRoleChange(ChangingRoleEventArgs ev)
		{
			if (ev.NewRole == RoleType.ClassD && EventHandlers.scp343.Count < 1)
			{
				if (new Random().Next(0, 101) <= Plugin.Singleton.Config.spawnChance)
				{
					EventHandlers.spawn343(ev.Player);
				}

				{
					
					float healAmount = ev.Player.Health * Plugin.Singleton.Config.healthPercent;
					if ((ev.Player.Health + healAmount) > ev.Player.MaxHealth)
						ev.Player.Health = ev.Player.MaxHealth;
					else
						ev.Player.Health += healAmount;
				}

				{
					ev.Player.Health += ev.Player.Health * Plugin.Singleton.Config.healthPercent;
				}
			}
			if (EventHandlers.scp343.Contains(ev.Player))
			{
				this.kill343(ev.Player);
			}
		}

		public void onItemGrab(PickingUpItemEventArgs ev)
		{
			if (EventHandlers.scp343.Contains(ev.Player) && !Plugin.Singleton.Config.canGrab)
			{
				ev.IsAllowed = false;
			}
		}

		public void onDetain(HandcuffingEventArgs ev)
		{
			if (EventHandlers.scp343.Contains(ev.Target))
			{
				ev.IsAllowed = false;
				ev.Cuffer.Broadcast(3, "You cannot cuff <color=red>SCP-343</color>", 0);
			}
		}

		public void onItemDrop(DroppingItemEventArgs ev)
		{
			if (EventHandlers.scp343.Contains(ev.Player) && !Plugin.Singleton.Config.canDrop)
			{
				ev.IsAllowed = false;
			}
		}

		public static void spawn343(Player p)
		{
			if (EventHandlers.scp343.Count < 1)
			{
				EventHandlers.scp343.Add(p);
				p.Broadcast(Plugin.Singleton.Config.broadcastLength, Plugin.Singleton.Config.spawnBroadcast, 0);

				if (Plugin.Singleton.Config.isInvincible)
				{
					p.IsGodModeEnabled = true;
				}
				if (Plugin.Singleton.Config.hasBypass)
				{
					p.IsBypassModeEnabled = true;
				}
				{
					Timing.CallDelayed(1f, delegate ()
					{
					});
				}
			}
		}

		public void onLeave(LeftEventArgs ev)
		{
			if (EventHandlers.scp343.Contains(ev.Player))
			{
				this.kill343(ev.Player);
			}
		}

		public void onWarheadExplosion()
		{
			foreach (Player player in EventHandlers.scp343)
			{
				player.Kill(DamageTypes.Nuke);
				player.Broadcast(Plugin.Singleton.Config.broadcastLength, Plugin.Singleton.Config.nukeDeath343, 0);
				if (EventHandlers.scp343.Contains(player))
				{
					this.kill343(player);
				}
			}
		}

		public void onDecontamination(DecontaminatingEventArgs ev)
		{
			foreach (Player player in EventHandlers.scp343)
			{
				player.Kill(DamageTypes.Decont);
				player.Broadcast(Plugin.Singleton.Config.broadcastLength, Plugin.Singleton.Config.deconDeath343, 0);
				if (EventHandlers.scp343.Contains(player))
				{
					this.kill343(player);
				}
			}
		}

		public void onRoundEnd(RoundEndedEventArgs ev)
		{
			EventHandlers.scp343.Clear();
		}

		public void onDeath(DiedEventArgs ev)
		{
			if (EventHandlers.scp343.Contains(ev.Target))
			{
				this.kill343(ev.Target);
			}
		}

		public void kill343(Player p)
		{
			if (EventHandlers.scp343.Contains(p))
			{
				EventHandlers.scp343.Remove(p);
			}
			if (Plugin.Singleton.Config.isInvincible)
			{
				p.IsGodModeEnabled = false;
			}
			if (Plugin.Singleton.Config.hasBypass)
			{
				p.IsBypassModeEnabled = false;
			}
			p.CustomInfo = null;
		}

		public static List<Player> scp343 = new List<Player>();
	}
}
