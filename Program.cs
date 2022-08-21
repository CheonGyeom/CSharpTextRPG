using System;

namespace TextRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            ClassType choice = ChooseClass();
            // 캐릭터 생성
            Player player;

            CreatePlayer(choice, out player);
            Console.WriteLine($"내 체력: {player.hp} 공격력: {player.attack}");

            // 사냥터로 가서 몬스터와 싸운다
            EnterGame(ref player);
        }
        enum ClassType
        {
            None = 0,
            Knight = 1,
            Archer = 2,
            Mage = 3
        }

        struct Player
        {
            public int hp;
            public int attack;
        }

        enum MonsterType
        {
            None = 0,
            Slime = 1,
            Orc = 2,
            Skeleton = 3
        }
        struct Monster
        {
            public int hp;
            public int attack;
        }
        // 직업 고르기
        static ClassType ChooseClass()
        {
            ClassType choice = ClassType.None;
            while (true)
            {
                Console.WriteLine("직업을 선택하세요.");
                Console.WriteLine("[1] 전사");
                Console.WriteLine("[2] 궁수");
                Console.WriteLine("[3] 마법사");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        choice = ClassType.Knight;
                        break;
                    case "2":
                        choice = ClassType.Archer;
                        break;
                    case "3":
                        choice = ClassType.Mage;
                        break;
                }
                if (choice != ClassType.None)
                    break;
            }
            return choice;
        }

        // 캐릭터생성
        static void CreatePlayer(ClassType choice, out Player player)
        {

            switch (choice)
            {
                case ClassType.Knight:
                    player.hp = 100;
                    player.attack = 15;
                    break;
                case ClassType.Archer:
                    player.hp = 75;
                    player.attack = 20;
                    break;
                case ClassType.Mage:
                    player.hp = 50;
                    player.attack = 25;
                    break;
                default:
                    player.hp = 0;
                    player.attack = 0;
                    break;
            }
        }

        // 게임 들어가기
        static void EnterGame(ref Player player)
        {
            while (true)
            {
                Console.WriteLine("마을에 도착했습니다.");
                Console.WriteLine("[1] 사냥터로 간다.");
                Console.WriteLine("[2] 로비로 돌아가기");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        EnterFeild(ref player);
                        break;
                    case "2":
                        return;
                }
            }
        }
        static void CreateRandomMonster(out Monster monster)
        {
            Random rand = new Random();//랜덤 수 받기
            int randMonster = rand.Next(1, 4);
            switch (randMonster)
            {
                case (int)MonsterType.Slime:
                    Console.WriteLine("슬라임이 등장했습니다.");
                    monster.hp = 25;
                    monster.attack = 6;
                    break;
                case (int)MonsterType.Orc:
                    Console.WriteLine("오크가 등장했습니다.");
                    monster.hp = 60;
                    monster.attack = 16;
                    break;
                case (int)MonsterType.Skeleton:
                    Console.WriteLine("스켈레톤이 등장했습니다.");
                    monster.hp = 45;
                    monster.attack = 10;
                    break;
                default:
                    monster.hp = 0;
                    monster.attack = 0;
                    break;
            }
        }

        //전투
        static void Fight(ref Player player, ref Monster monster)
        {
            while (true)
            {
                // 플레이어가 몬스터 공격
                monster.hp -= player.attack;
                if (monster.hp <= 0)
                {
                    Console.WriteLine("승리했습니다!");
                    Console.WriteLine($"남은 체력 : {player.hp}");
                    break;
                }

                // 몬스터 반격
                player.hp -= monster.attack;
                if (player.hp <= 0)
                {
                    Console.WriteLine("몬스터에게 당했습니다.");
                    break;
                }

            }
        }

        //사냥터로 이동
        static void EnterFeild(ref Player player)
        {
            while (true)
            {
                Console.WriteLine("사냥터에 도착했습니다.");

                // 몬스터 생성
                // 랜덤으로 1~3 몬스터 중 하나 스폰
                Monster monster;
                CreateRandomMonster(out monster);

                Console.WriteLine("[1] 싸운다");
                Console.WriteLine("[2] 도망친다");

                string input = Console.ReadLine();
                if (input == "1") 
                {
                    Fight(ref player, ref monster);
                }
                else if (input == "2")
                {
                    // 33% 확률로 도망
                    Random rand = new Random();
                    int randValue = rand.Next(0, 101);
                    if (randValue <= 33)
                    {
                        Console.WriteLine("무사히 도망쳤다!");
                        break;
                    }
                    else
                    {
                        Fight(ref player, ref monster);
                    }
                }
            }

        }
    }

}
