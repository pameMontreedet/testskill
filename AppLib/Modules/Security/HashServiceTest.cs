using System;
using System.Security.Cryptography;
using AppLib.Core;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Moq;
using Xunit;

namespace AppLib.Modules.Security
{
    public class HashServiceTest
    {
        [Fact]
        public void TestCreateHash_GivenTextInput_ExpectNonEmptyHashString()
        {
            var hashService = new HashService();
            var pass1Hash = hashService.Hash("pass1");
            Assert.NotEmpty(pass1Hash);

            var pass2Hash = hashService.Hash("pass2");
            Assert.NotEmpty(pass2Hash);

            var pass3Hash = hashService.Hash("pass3");
            Assert.NotEmpty(pass3Hash);

            Assert.NotEqual(pass1Hash, pass2Hash);
            Assert.NotEqual(pass2Hash, pass3Hash);
        }

        [Fact]
        public void TestCreateHash_GivenEmptyInput_ExpectException()
        {
            var hashService = new HashService();
            Exception ex = Assert.Throws<ServiceException>(() => hashService.Hash(""));
            Assert.Equal("Invalid Operation Exception, Hash input cannot be an empty string", ex.Message);
        }

        [Fact]
        public void TestCheckMatch_GivenCorrectPassword_ExpectPasswordIsMatched()
        {
            var hashService = new HashService();
            var hash = hashService.Hash("password");
            Assert.True(hashService.CheckMatch("password", hash));

            hash = hashService.Hash("new-password");
            Assert.True(hashService.CheckMatch("new-password", hash));
        }

        [Fact]
        public void TestCheckMatch_GivenIncorrectPassword_ExpectPasswordIsNotMatched()
        {
            var hashService = new HashService();
            var hash = hashService.Hash("password");
            Assert.False(hashService.CheckMatch("wrong-password", hash));

            hash = hashService.Hash("new-password");
            Assert.False(hashService.CheckMatch("wrong-new-password", hash));
        }


    }
}