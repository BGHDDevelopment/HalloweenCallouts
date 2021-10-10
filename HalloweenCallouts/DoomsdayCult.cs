using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API;
using FivePD.API.Utils;


namespace HalloweenCallouts
{
    [CalloutProperties("Doomsday Cult", "BGHDDevelopment", "1.0")]
    public class DoomsdayCult : Callout
    {
        Ped driver, passenger, passenger2;
        private Vehicle car;
        
        public DoomsdayCult()
        {
            Random rnd = new Random();
            float offsetX = rnd.Next(100, 700);
            float offsetY = rnd.Next(100, 700);
            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));
            ShortName = "Doomsday Cult";
            CalloutDescription = "A doomsday cult has been found and they are attacking!";
            ResponseCode = 3;
            StartDistance = 150f;
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            driver = await SpawnPed(RandomUtils.GetRandomPed(), Location + 2);
            passenger = await SpawnPed(RandomUtils.GetRandomPed(), Location + 1);
            passenger2 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 1);
            car = await SpawnVehicle(VehicleHash.Technical2, Location);
            driver.SetIntoVehicle(car, VehicleSeat.Driver);
            passenger2.SetIntoVehicle(car, VehicleSeat.Passenger);
            passenger.SetIntoVehicle(car, VehicleSeat.Any);
            driver.AlwaysKeepTask = true;
            driver.BlockPermanentEvents = true;
            passenger.AlwaysKeepTask = true;
            passenger.BlockPermanentEvents = true;
            passenger2.AlwaysKeepTask = true;
            passenger2.BlockPermanentEvents = true;
            driver.Weapons.Give(WeaponHash.Pistol, 20, true, true);
            passenger.Weapons.Give(WeaponHash.SMG, 150, true, true);
            passenger2.Weapons.Give(WeaponHash.SMG, 150, true, true);
            API.SetDriveTaskMaxCruiseSpeed(driver.GetHashCode(), 35f);
            API.SetDriveTaskDrivingStyle(driver.GetHashCode(), 524852);
            driver.Task.FleeFrom(player);
            car.AttachBlip();
            driver.AttachBlip();
            passenger.AttachBlip();
            passenger2.AttachBlip();
            API.Wait(6000);
            passenger.Task.FightAgainst(player);
            passenger2.Task.FightAgainst(player);
        }

        public async override Task OnAccept()
        {
            InitBlip();
            UpdateData();
        }
    }
}