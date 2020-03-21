using System;
using Xunit;
using ObserverStandard;
using MvvmStandard;
using System.Windows.Input;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        protected class SubjectModel : Subject
        {
            public string MyMessage { get; set; }
        }

        protected class ObserverModel : Observer
        {
            public override void Update(string mode, string msg)
            {
                Assert.Equal("mode", mode);
                Assert.Equal("message", msg);
            }
        }


        [Fact]
        public void TestObserver()
        {
            SubjectModel sub = new SubjectModel();
            ObserverModel obs = new ObserverModel();
            sub.Attach(obs);
            sub.Notify("mode", "message");
        }


        protected class TestVM : ViewModelBase
        {
            public ICommand TestCommand { get; set; }

            private string _Message;

            public string Message { get => _Message; set { _Message = value; OnPropertyChanged("Message"); } }

            public TestVM()
            {
                TestCommand = new RelayCommand(new Action<Object>((o) => { Message = o.ToString(); }));
            }
        }

        [Fact]
        public void TestMVVM()
        {
            TestVM vm = new TestVM();

            vm.PropertyChanged += (s, e) => 
            {
                TestVM v = (TestVM)s;
                Assert.Equal("test", v.Message);
                Assert.Equal("Message", e.PropertyName);
            };

            vm.TestCommand.Execute("test");
            Assert.Equal("test", vm.Message);
        }

    }
}
