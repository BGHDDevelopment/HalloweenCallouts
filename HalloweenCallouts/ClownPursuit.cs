using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;


namespace HalloweenCallouts
{
    [CalloutProperties("Clown Pursuit", "BGHDDevelopment", "1.0")]
    public class ClownPursuit : Callout
    {
        Ped clown;
        private Vehicle car;
        
        public ClownPursuit()
        {
            Random rnd = new Random();
            float offsetX = rnd.Next(100, 700);
            float offsetY = rnd.Next(100, 700);
            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));
            ShortName = "Clown Pursuit";
            CalloutDescription = "We have a pursuit of a clown!";
            ResponseCode = 3;
            StartDistance = 150f;
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            clown = await SpawnPed(PedHash.Clown01SMY, Location + 2);
            car = await SpawnVehicle(VehicleHash.Speedo2, Location);
            clown.SetIntoVehicle(car, VehicleSeat.Driver);
            clown.Weapons.Give(WeaponHash.Pistol, 100, true, true);
            clown.Task.FleeFrom(player);
            car.AttachBlip();
            clown.AttachBlip();
            var pursuit = Pursuit.RegisterPursuit(clown);
            clown.AlwaysKeepTask = true;
            clown.BlockPermanentEvents = true;
        }

        public async override Task OnAccept()
        {
            InitBlip();
            UpdateData();
        }
    }
}