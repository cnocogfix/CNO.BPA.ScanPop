using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms.VisualStyles;

namespace CNO.BPA.ScanPop
{
    class DTPickerOverride : System.Windows.Forms.DateTimePicker
    {
       public DTPickerOverride() : base()
        {
            this.SetStyle(ControlStyles.UserPaint, true);            
        }

        /// <summary>
        ///     Gets or sets the foreground color of the control
        /// </summary>
        [Category("Appearance"), Description("The color of the selected date")]
        [Browsable(true)]
        public override Color ForeColor
        {
            get {return base.ForeColor;}
            set {base.ForeColor = value;}
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
                        
            //The dropDownRectangle defines position and size of dropdownbutton block, 
            //the width is fixed to 17 and height to 16. The dropdownbutton is aligned to right
            Rectangle dropDownRectangle = new Rectangle(ClientRectangle.Width - 17, 0, 17, 16);
            ComboBoxState visualState;
            Brush foreBrush;
            

            //When the control is enabled the brush is set to Backcolor, 
            //otherwise to color stored in _backDisabledColor
            if (this.Enabled) 
            {              
                 visualState = ComboBoxState.Normal;
                 foreBrush = new SolidBrush(this.ForeColor);
            }
            else 
            {                              
                visualState = ComboBoxState.Disabled;
                foreBrush = new SolidBrush(Color.FromKnownColor(KnownColor.Control));
            }
            
            //Drawing the datetime text
            g.DrawString(this.Text, this.Font, foreBrush, 0, 2);

            //Drawing the dropdownbutton using ComboBoxRenderer
            ComboBoxRenderer.DrawDropDownButton(g, dropDownRectangle, visualState);

            g.Dispose();
            foreBrush.Dispose();
        }
    }
}
