using Client.Messages;
using Client.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Client.ViewModels
{
    public class SearchContactViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private bool _isSearchByName;
        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public bool IsSearchByName
        {
            get => _isSearchByName;
            set => SetProperty(ref _isSearchByName, value);
        }

        public SearchContactView Window { get; set; }

        public DelegateCommand SearchContactsCommand { get; private set; }

        public SearchContactViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            SearchContactsCommand = new DelegateCommand(() => SearchContacts());

            IsSearchByName = true;
        }

        private void SearchContacts()
        {
            if (string.IsNullOrEmpty(SearchText))
                return;

            if (IsSearchByName)
                _eventAggregator.GetEvent<SearchContactByNameMessage>().Publish(SearchText);
            else
                _eventAggregator.GetEvent<SearchContactByTelephoneMessage>().Publish(SearchText);

            Window.Close();
        }
    }
}