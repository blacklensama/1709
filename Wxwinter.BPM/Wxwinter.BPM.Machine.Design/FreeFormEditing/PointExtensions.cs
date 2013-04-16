﻿//------------------------------------------------------------

//------------------------------------------------------------

namespace Wxwinter.BPM.Machine.Design.FreeFormEditing
{
    using System.Windows;

    internal static class PointExtensions
    {
        public static bool IsEqualTo(this Point point1, Point point2)
        {
            return DesignerGeometryHelper.DistanceBetweenPoints(point1, point2) < DesignerGeometryHelper.EPS;
        }
    }
}