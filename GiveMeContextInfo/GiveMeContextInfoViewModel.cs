using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace GiveMeContextInfo
{
    public class GiveMeContextInfoViewModel : INotifyPropertyChanged
    {
        public GiveMeContextInfoViewModel ()
        {
        }

        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;

        private Command _takePictureCommand;
        public Command TakePictureCommand
        {
            get
            {
                return _takePictureCommand ??
                    (_takePictureCommand = new Command (async () => await ExecuteTakePictureCommandAsync ()));
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            private set
            {
                _description = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged (this,
                        new PropertyChangedEventArgs ("Description"));
                }
            }
        }

        private ObservableCollection<EntityLink> _contextInfo = new ObservableCollection<EntityLink> ();
        public ObservableCollection<EntityLink> ContextInfo
        {
            get { return _contextInfo; }
        }

        private bool _hasResults = false;
        public bool HasResults
        {
            get { return _hasResults; }
            private set
            {
                _hasResults = value;
                if (PropertyChanged != null) {
                    PropertyChanged (this,
                        new PropertyChangedEventArgs ("HasResults"));
                }
            }
        }

        // [For demo purposes only]
        // Ugly way to handle a "Click" event in Xamarin.Forms when using a ListView
        private EntityLink _selectedContextInfo;
        public EntityLink SelectedContextInfo
        {
            get { return _selectedContextInfo; }
            set
            {
                _selectedContextInfo = value;

                if (_selectedContextInfo == null)
                    return;

                var url = _selectedContextInfo.WikipediaID.Replace(" ", "%20"); // Simple URL encode
                Device.OpenUri (new Uri ($"https://en.wikipedia.org/wiki/{url}"));
                SelectedContextInfo = null;
            }
        }

        #endregion

        async Task ExecuteTakePictureCommandAsync ()
        {
            try
            {
                HasResults = false;
                ContextInfo.Clear ();

                var photo = await MediaService.TakePhotoAsync ();
                Description = "Let me think...";

                var recognizedText = await ComputerVisionService.RecognizeTextAsync (photo);
                Description = "Got it! Determing context info...";
                System.Diagnostics.Debug.WriteLine ($"RecognizedText: {recognizedText}");

                var linkedEntities = await EntityLinkingService.LinkEntityAsync (recognizedText);
                Description = $"Found more info about {linkedEntities.Count} items:";

                HasResults = true;
                foreach (var linkedEntity in linkedEntities)
                {
                    ContextInfo.Add (linkedEntity);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine ($"ERROR: {ex.Message}");
                Description = "Sorry, I can't give you any info.";
            }
        }
    }
}
