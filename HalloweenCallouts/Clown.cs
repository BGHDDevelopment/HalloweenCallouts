using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;


namespace HalloweenCallouts
{
    [CalloutProperties("Clown Sighting", "BGHDDevelopment", "1.0")]
    public class Clown : Callout
    {
        Ped clown;

        public Clown()
        {
            Random rnd = new Random();
            float offsetX = rnd.Next(100, 700);
            float offsetY = rnd.Next(100, 700);
            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));
            ShortName = "Clown Sighting";
            CalloutDescription = "A clown has been sighted!";
            ResponseCode = 2;
            StartDistance = 150f;
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            clown = await SpawnPed(PedHash.Clown01SMY, Location + 2);
            clown.AlwaysKeepTask = true;
            clown.BlockPermanentEvents = true;
            clown.Weapons.Give(WeaponHash.Bat, 100, true, true);
            clown.AttachBlip();
            clown.Task.FightAgainst(player);
        }

        public async override Task OnAccept()
        {
            InitBlip();
            UpdateData();
        }
    }
}