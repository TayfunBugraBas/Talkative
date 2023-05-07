using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talkative.Source.Models;

namespace Talkative.Source.Globals
{
    public class Globals
    {
        public ObservableCollection<WordModel> GetWordsList = new ObservableCollection<WordModel>();
    }
}
