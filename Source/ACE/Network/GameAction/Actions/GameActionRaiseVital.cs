﻿namespace ACE.Network.GameAction.Actions
{
    [GameAction(GameActionOpcode.RaiseVital)]
    public class GameActionRaiseVital : GameActionPacket
    {
        private Vital vital;
        private uint xpSpent;

        public GameActionRaiseVital(Session session, ClientPacketFragment fragment) : base(session, fragment) { }

        public override void Read()
        {
            vital = (Vital)fragment.Payload.ReadUInt32();
            xpSpent = fragment.Payload.ReadUInt32();
        }

        public override void Handle()
        {
            Entity.Enum.Ability ability = Entity.Enum.Ability.None;
            switch (vital)
            {
                case Vital.MaxHealth:
                    ability = Entity.Enum.Ability.Health;
                    break;
                case Vital.MaxStamina:
                    ability = Entity.Enum.Ability.Stamina;
                    break;
                case Vital.MaxMana:
                    ability = Entity.Enum.Ability.Mana;
                    break;
                default:
                    ChatPacket.SendSystemMessage(session, $"Unable to Handle GameActionRaiseVital for vital {vital}");
                    return;
            }
            session.Player.SpendXp(ability, xpSpent);
        }
    }
}
