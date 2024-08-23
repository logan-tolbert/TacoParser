using System;
using Xunit;

namespace LoggingKata.Test
{
    public class TacoParserTests
    {
        [Fact]
        public void ShouldReturnNonNullObject()
        {
            //Arrange
            var tacoParser = new TacoParser();

            //Act
            var actual = tacoParser.Parse("34.073638, -84.677017, Taco Bell Acwort...");

            //Assert
            Assert.NotNull(actual);

        }


        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", -84.677017)]
        [InlineData("34.376395,-84.913185,Taco Bell Adairsvill...", -84.913185)]
        [InlineData("33.856516,-84.023179,Taco Bell Snellvill...", -84.023179)]
        [InlineData("30.906033,-87.79328,Taco Bell Bay Minett...", -87.79328)]
        [InlineData("30.409891,-84.280756,Taco Bell Tallahassee...", -84.280756)]
        [InlineData("34.026711,-84.049344,Taco Bell Suwanee...", -84.049344)]
        [InlineData("30.506038,-84.249817,Taco Bell Tallahasse...", -84.249817)]
        [InlineData("33.824114,-84.107251,Taco Bell Stone Mountai...", -84.107251)]
       
        public void ShouldParseLongitude(string line, double expected)
        {
            var tacoParser = new TacoParser();
            
            var actual = tacoParser.Parse(line);
            
            Assert.Equal(expected, actual.Location.Longitude);
        }


        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", 34.073638)]
        [InlineData("34.376395,-84.913185,Taco Bell Adairsvill...", 34.376395)]
        [InlineData("33.856516,-84.023179,Taco Bell Snellvill...", 33.856516)]
        [InlineData("30.906033,-87.79328,Taco Bell Bay Minett...", 30.906033)]
        [InlineData("30.409891,-84.280756,Taco Bell Tallahassee...", 30.409891)]
        [InlineData("34.026711,-84.049344,Taco Bell Suwanee...", 34.026711)]
        [InlineData("30.506038,-84.249817,Taco Bell Tallahasse...", 30.506038)]
        [InlineData("33.824114,-84.107251,Taco Bell Stone Mountai...", 33.824114)]

        public void ShouldParseLatitude(string line, double expected)
        {
            var tacoParser = new TacoParser();
        
            var actual = tacoParser.Parse(line);
          
            Assert.Equal(expected, actual.Location.Latitude);
        }
    }
}
