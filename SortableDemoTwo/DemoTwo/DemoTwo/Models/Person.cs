using MintPlayer.Xamarin.Forms.SortableListView.DemoTwo.Enums;
using MintPlayer.Xamarin.Forms.SortableListView.DemoTwo.ViewModels;
using System;

namespace MintPlayer.Xamarin.Forms.SortableListView.DemoTwo.Models
{
    public class Person : BaseModel, ICloneable
    {
        #region Name
        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        #endregion
        #region Position
        private string position;
        public string Position
        {
            get => position;
            set => SetProperty(ref position, value);
        }
        #endregion
        #region BirthDay
        private DateTime birthDay;
        public DateTime BirthDay
        {
            get => birthDay;
            set => SetProperty(ref birthDay, value);
        }
        #endregion
        #region Gender
        private eGender gender;
        public eGender Gender
        {
            get => gender;
            set => SetProperty(ref gender, value);
        }
        #endregion

        public override string ToString() => name;

        public object Clone()
        {
            return new Person
            {
                birthDay = birthDay,
                gender = gender,
                name = name,
                position = position,
            };
        }
    }
}
