using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.Infrastructure
{
    public enum MouseButtons
    {
        Left = 0,
        Middle = 1,
        Right = 2
    }

    public static class MouseButtonsH
    {
        public static MouseButtons ToMouseButton(this long mouseBtnIdx)
        {
            var mouseBtn = (MouseButtons)mouseBtnIdx;
            return mouseBtn;
        }

        public static bool IsMouseButton(
            this long mouseBtnIdx,
            MouseButtons mouseBtn)
        {
            bool retVal = mouseBtn == (MouseButtons)mouseBtnIdx;
            return retVal;
        }

        public static bool IsLeftMouseButton(
            this long mouseBtnIdx)
        {
            bool retVal = mouseBtnIdx.IsMouseButton(MouseButtons.Left);
            return retVal;
        }

        public static bool IsRightMouseButton(
            this long mouseBtnIdx)
        {
            bool retVal = mouseBtnIdx.IsMouseButton(MouseButtons.Right);
            return retVal;
        }

        public static bool IsMiddleMouseButton(
            this long mouseBtnIdx)
        {
            bool retVal = mouseBtnIdx.IsMouseButton(MouseButtons.Middle);
            return retVal;
        }
    }
}
