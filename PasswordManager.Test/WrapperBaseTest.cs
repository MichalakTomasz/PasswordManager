using PasswordManager.BaseClasses;
using PasswordManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PasswordManager.Test
{
    
    public class WrapperBaseTest
    {
        [Fact]
        public void ShouldSetIsChangeProperty()
        {
            var name = "Roman";
            var wrapper = new PasswordWrapper();
            wrapper.Name = name;
            Assert.True(wrapper.IsChangedName);
            Assert.True(wrapper.IsChanged);
        }
        
        [Fact]
        public void ShouldSetOriginalValueAfterChange()
        {
            var name = "Roman";
            var wrapper = new PasswordWrapper();
            wrapper.Name = name;
            wrapper.Name = "Anna";
            Assert.Equal(name, wrapper.OriginalValueName);
            Assert.Equal("Anna", wrapper.Name);
        }

        [Fact]
        public void ShouldHadIsChangetPropertyTrueAfterAcceptChangesTest()
        {
            var wrapper = new PasswordWrapper();
            wrapper.Password = "3423";
            Assert.True(wrapper.IsChanged);
            wrapper.AcceptChanges();
            Assert.False(wrapper.IsChanged);
        }
    }
}
