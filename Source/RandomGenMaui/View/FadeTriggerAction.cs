using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jukusui.RandomGen.View
{
    public class FadeTriggerAction : TriggerAction<VisualElement>
    {

        public bool IsEnter { get; set; }
        public uint Length { get; set; }
        public double Diff { get; set; }

        protected override void Invoke(VisualElement sender)
        {
            if (IsEnter)
                sender.IsVisible = true;
            sender.Animate(nameof(FadeTriggerAction),
                animation: new Animation(d =>
                {
                    var val = IsEnter ? d : 1.0 - d;
                    sender.TranslationY = Diff * (1.0 - val);
                    sender.Opacity = val;
                }), length: Length, easing: (IsEnter ? Easing.CubicIn : Easing.CubicInOut),
                finished: (s, e) =>
                {
                    if (!IsEnter)
                        sender.IsVisible = false;
                });
        }
    }
}
