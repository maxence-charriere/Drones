namespace Drones.ARDrone.Data.Navdata
{
    public enum ControlState : ushort
    {
        Default,
        Init,
        Landed,
        Flying,
        Hovering,
        Test,
        TransTakeOff,
        TransGoToFix,
        TransLanding,
        TransLooping
    }
}
