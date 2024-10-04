using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProgressViewApp
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private float _progress;
        private Color _ringColor = Colors.Blue;
        private Color _thumbColor = Colors.Red;  // Add this

        public float Progress
        {
            get => _progress;
            set
            {
                if (_progress != value)
                {
                    _progress = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color RingColor
        {
            get => _ringColor;
            set
            {
                if (_ringColor != value)
                {
                    _ringColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color ThumbColor  // Add this
        {
            get => _thumbColor;
            set
            {
                if (_thumbColor != value)
                {
                    _thumbColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
