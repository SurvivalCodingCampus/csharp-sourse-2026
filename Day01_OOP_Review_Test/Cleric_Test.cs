namespace Day01_OOP_Review_Test;

[TestFixture]
public class Cleric_Test
{
    [Fact]
        public void Constructor_WithEmptyName_ShouldThrowException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => new Clerric(null));
            Assert.Throws<ArgumentException>(() => new Cleric(""));
            Assert.Throws<ArgumentException>(() => new Cleric(" "));
        }

        [Fact]
        public void Constructor_WithValidName_ShouldInitializeCorrectly()
        {
            // Arrange & Act
            var cleric = new Cleric("Arthur");

            // Assert
            Assert.Equal("Arthur", cleric.Name);
            Assert.Equal(Cleric.MaxHP, cleric.HP);
            Assert.Equal(Cleric.MaxMP, cleric.MP);
        }

        [Fact]
        public void SelfAid_WhenHasEnoughMP_ShouldRestoreHPAndConsumeMP()
        {
            // Arrange
            var cleric = new Cleric("Arthur");
            cleric.HP = 10; // HP를 일부 감소시킴 (테스트를 위해 내부 상태 조작이 필요하다면 접근 제한자 수정 필요)
            // 위 코드는 캡슐화를 위해 property가 private set 이므로, 
            // 실제 테스트를 위해 클래스에 HP 감소 메서드가 있거나 생성 시 조작 가능해야 함.
            // 여기서는 로직 검증을 위해 MP만 확인하는 방식으로 진행.
            
            int initialMP = cleric.MP;
            
            // Act
            cleric.SelfAid();

            // Assert
            Assert.Equal(initialMP - 5, cleric.MP);
            Assert.Equal(Cleric.MaxHP, cleric.HP);
        }

        [Fact]
        public void Pray_ShouldNotExceedMaxMP()
        {
            // Arrange
            var cleric = new Cleric("Arthur");
            // MP를 9로 설정 (MaxMP가 10이므로 1만 더 회복 가능해야 함)
            // 클래스 구조상 MP를 직접 수정할 수 없으므로, 
            // SelfAid를 사용하여 MP를 5로 만든 뒤 테스트 진행
            cleric.SelfAid(); // MP: 10 -> 5
            
            // Act
            int recovered = cleric.Pray(10); // 10초 기도하면 10+0~2 만큼 회복 시도

            // Assert
            Assert.True(cleric.MP <= Cleric.MaxMP);
            // 5에서 10을 넘을 수 없으므로 실제 회복량은 최대 5임
            Assert.True(recovered <= 5); 
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Pray_ShouldReturnActualRecoveredAmount(int seconds)
        {
            // Arrange
            var cleric = new Cleric("Arthur");
            // MP를 5로 만듦
            cleric.SelfAid(); 
            int mpBeforePray = cleric.MP;

            // Act
            int recovered = cleric.Pray(seconds);

            // Assert
            Assert.Equal(mpBeforePray + recovered, cleric.MP);
            Assert.True(recovered >= seconds); // 최소 seconds 만큼은 회복되어야 함
        }
    
}