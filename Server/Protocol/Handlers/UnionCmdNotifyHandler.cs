
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSFreedom.Common.Protocol;
using YSFreedom.Common.Protocol.Messages;
using YSFreedom.Server.Game;
using Google.Protobuf;

namespace YSFreedom.Server.Protocol.Handlers
{
	public class UnionCmdNotifyHandler : IYuanShenHandler
	{
		public UnionCmdNotifyHandler() { }
		public async Task HandleAsync(YuanShenPacket packet, Player player)
		{
			MsgUnionCmdNotify req = (MsgUnionCmdNotify)packet;

			foreach (var item in req.PacketBody.CmdList)
            {
				Console.WriteLine($"got MessageId {(EMsgType)item.MessageId}");
				Console.WriteLine(item.ToString());

				switch (item.MessageId)
                {
					case 1102:
						AbilityInvocationsNotify abilityInvocationsNotify = AbilityInvocationsNotify.Parser.ParseFrom(item.Body);
						Console.WriteLine(abilityInvocationsNotify.ToString());
						break;
					case 1112:
						ClientAbilityChangeNotify clientAbilityChangeNotify = ClientAbilityChangeNotify.Parser.ParseFrom(item.Body);
						Console.WriteLine(clientAbilityChangeNotify.ToString());
						break;
					case 350:
						CombatInvocationsNotify combatInvocationsNotify = CombatInvocationsNotify.Parser.ParseFrom(item.Body);
						Console.WriteLine(combatInvocationsNotify.ToString());

						foreach (var combatitem in combatInvocationsNotify.InvokeList)
						{
							EntityMoveInfo entityMoveInfo = EntityMoveInfo.Parser.ParseFrom(combatitem.CombatData);
							Console.WriteLine(entityMoveInfo.ToString());
							if ((combatitem.ArgumentType == CombatTypeArgument.EntityMove) && (entityMoveInfo.EntityId == 16779279)) // current player entity id
                            {
								// save player location TOTALLY IO FRIENDLY :D
								PlayerEnterSceneNotify playerEnterSceneNotify = PlayerEnterSceneNotify.Parser.ParseFrom(File.ReadAllBytes("PlayerEnterSceneNotify.bin"));
								playerEnterSceneNotify.Pos = entityMoveInfo.MotionInfo.Pos;
								File.WriteAllBytes("PlayerEnterSceneNotify.bin", playerEnterSceneNotify.ToByteArray());
								SceneEntityAppearNotify sceneEntityAppearNotify = SceneEntityAppearNotify.Parser.ParseFrom(File.ReadAllBytes("SceneEntityAppearNotify.bin"));
								foreach (var mentity_list in sceneEntityAppearNotify.EntityList)
                                {
									if (mentity_list.EntityType == ProtEntityType.ProtEntityAvatar && mentity_list.EntityId == 16779279)
                                    {
										mentity_list.MotionInfo.Pos = entityMoveInfo.MotionInfo.Pos;
										mentity_list.MotionInfo.Rot = entityMoveInfo.MotionInfo.Rot;
									}
                                }
								File.WriteAllBytes("SceneEntityAppearNotify.bin", sceneEntityAppearNotify.ToByteArray());
							}
						}
						break;
					default:
						break;
                }
            }

			return;
		}
	}
}

