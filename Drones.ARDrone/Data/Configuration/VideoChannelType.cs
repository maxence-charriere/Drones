namespace Drones.ARDrone.Data.Configuration
{
    public enum VideoChannelType
    {
        /// <summary> Selects the horizontal camera. </summary>
        Horizontal = 0,

        /// <summary> Selects the vertical camera. </summary>
        Vertical = 1, 

        /// <summary> AR.Drone 1.0 only. </summary>
        HorizontalPlusSmallVertical = 2,

        /// <summary> AR.Drone 1.0 only. </summary>
        VerticalPlusSmallHorizontal = 3,

        /// <summary> Selects the next available format among those above. </summary>
        Next = 4 
    }
}
