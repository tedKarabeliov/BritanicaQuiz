namespace BritanicaQuiz.Data.Migrations
{
    using System.Data.Entity.Migrations;

    using BritanicaQuiz.Data;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using System.Linq;
    using BritanicaQuiz.Model;
    using System;
    using BritanicaQuiz.Model.Questions;
    using System.Configuration;

    internal sealed class Configuration : DbMigrationsConfiguration<BritanicaQuizDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BritanicaQuizDbContext db)
        {
            this.AssignUsersAndRoles(db);
            db.SaveChanges();

            var usingTestConfiguration = bool.Parse(ConfigurationManager.AppSettings["TestConfiguration"]);

            if (usingTestConfiguration)
            {
                this.AssignTestConfiguration(db);
            }
            else
            {
                this.AssignProductionConfiguration(db);
            }
        }

        private void AssignTestConfiguration(BritanicaQuizDbContext db)
        {
            if (!(db.Quizzes.Any()))
            {
                var quizC1 = new Quiz() { Name = "C1", MaximumPoints = 80 };
                var quizB2Plus = new Quiz() { Name = "B2+", MaximumPoints = 80, NextQuiz = quizC1 };
                var quizB2 = new Quiz() { Name = "B2", MaximumPoints = 80, NextQuiz = quizB2Plus };
                var quizB1 = new Quiz() { Name = "B1", MaximumPoints = 80, NextQuiz = quizB2 };
                var quizA2 = new Quiz() { Name = "A2", MaximumPoints = 80, NextQuiz = quizB1 };
                var quizA1 = new Quiz() { Name = "A1", MaximumPoints = 4, NextQuiz = quizB1 };
                var quizA0 = new Quiz() { Name = "A0", MaximumPoints = 36, NextQuiz = quizA1 };

                db.Quizzes.AddOrUpdate(quizC1, quizB2Plus, quizB2, quizB1, quizA2, quizA1, quizA0);
                db.SaveChanges();

                var questionSetHowOldAreYou = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 0, 15), Quiz = quizA0 };
                var questionSetOnePlusOne = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 0, 15), Quiz = quizA0 };
                var questionSetPesho = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 0, 15), Quiz = quizA0 };
                var questionSetInput = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 0, 15), Quiz = quizA0 };
                var questionSetReading = new QuestionSet() { Text = "<p>Some very looong text... ?</p>", SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 0, 15), Quiz = quizA0 };
                var questionSetA2 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 0, 15), Quiz = quizA1 };

                db.QuestionSets.AddOrUpdate(questionSetHowOldAreYou, questionSetOnePlusOne, questionSetPesho, questionSetInput, questionSetReading);

                db.SaveChanges();

                var questionHowOldAreYou = new InputAnswerQuestion() { Text = "How old are []?", QuestionSet = questionSetHowOldAreYou };
                var questionOnePlusOne = new OneAnswerQuestion() { Text = "One plus one ? <div>[]</div><div>[]</div><div>[]</div><div>[]</div>", QuestionSet = questionSetOnePlusOne };
                var questionPesho = new MultipleAnswersQuestion() { Text = "Pesho is ... <div>[]</div><div>[]</div><div>[]</div><div>[]</div>", QuestionSet = questionSetPesho };
                var questionInput = new InputAnswerQuestion() { Text = "How [] are []?", QuestionSet = questionSetInput };

                //var questionReadingOne = new OneAnswerQuestion() { Text = "Some very looong text... ?|3The Beatles didn’t come from London.|3Vancouver is bigger than all the other Canadian cities.|3Jacob was born in Vancouver.", QuestionSet = questionSetReading };

                var questionReadingOne = new OneAnswerQuestion() { Text = "<p>The Beatles didn’t come from London. <p>[]</p> <p>[]</p> <p>[]</p></p>", QuestionSet = questionSetReading };
                var questionReadingTwo = new OneAnswerQuestion() { Text = "<p>Vancouver is bigger than all the other Canadian cities. <p>[]</p> <p>[]</p> <p>[]</p></p>", QuestionSet = questionSetReading };
                var questionReadingThree = new OneAnswerQuestion() { Text = "<p>Jacob was born in Vancouver. <p>[]</p> <p>[]</p> <p>[]</p></p>", QuestionSet = questionSetReading };

                var questionA2 = new InputAnswerQuestion() { Text = "Very []?", QuestionSet = questionSetA2 };


                db.Questions.AddOrUpdate(questionHowOldAreYou);
                db.Questions.AddOrUpdate(questionInput);
                db.Questions.AddOrUpdate(questionOnePlusOne);
                db.Questions.AddOrUpdate(questionPesho);
                db.Questions.AddOrUpdate(questionReadingOne);
                db.Questions.AddOrUpdate(questionReadingTwo);
                db.Questions.AddOrUpdate(questionReadingThree);
                db.Questions.AddOrUpdate(questionA2);
                db.SaveChanges();

                var answerWithInput = new Answer() { Text = "you", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionHowOldAreYou };
                db.Answers.AddOrUpdate(answerWithInput);
                db.Answers.AddOrUpdate(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionHowOldAreYou });


                db.Answers.AddOrUpdate(new Answer { Text = "cool", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionInput });
                db.Answers.AddOrUpdate(new Answer { Text = "you", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionInput });
                db.Answers.AddOrUpdate(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionInput });

                db.Answers.AddOrUpdate(new Answer() { Text = "One", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionOnePlusOne });
                db.Answers.AddOrUpdate(new Answer() { Text = "Two", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionOnePlusOne });
                db.Answers.AddOrUpdate(new Answer() { Text = "Three", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionOnePlusOne });
                db.Answers.AddOrUpdate(new Answer() { Text = "Four", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionOnePlusOne });
                db.Answers.AddOrUpdate(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionOnePlusOne });

                db.Answers.AddOrUpdate(new Answer() { Text = "Programmer", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionPesho });
                db.Answers.AddOrUpdate(new Answer() { Text = "Driver", IsCorrect = false, Points = 4, NegativePoints = 4, Question = questionPesho });
                db.Answers.AddOrUpdate(new Answer() { Text = "Developer", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionPesho });
                db.Answers.AddOrUpdate(new Answer() { Text = "Painter", IsCorrect = false, Points = 4, NegativePoints = 4, Question = questionPesho });
                db.Answers.AddOrUpdate(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionPesho });

                db.Answers.AddOrUpdate(new Answer() { Text = "True", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionReadingOne });
                db.Answers.AddOrUpdate(new Answer() { Text = "False", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionReadingOne });
                db.Answers.AddOrUpdate(new Answer() { Text = "Doesn't say", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionReadingOne });
                db.Answers.AddOrUpdate(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionReadingOne });

                db.Answers.AddOrUpdate(new Answer() { Text = "True", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionReadingTwo });
                db.Answers.AddOrUpdate(new Answer() { Text = "False", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionReadingTwo });
                db.Answers.AddOrUpdate(new Answer() { Text = "No information", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionReadingTwo });
                db.Answers.AddOrUpdate(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionReadingTwo });

                db.Answers.AddOrUpdate(new Answer() { Text = "True", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionReadingThree });
                db.Answers.AddOrUpdate(new Answer() { Text = "False", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionReadingThree });
                db.Answers.AddOrUpdate(new Answer() { Text = "No information", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionReadingThree });
                db.Answers.AddOrUpdate(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionReadingThree });

                db.Answers.AddOrUpdate(new Answer() { Text = "good", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2 });
                db.Answers.AddOrUpdate(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2 });

                db.SaveChanges();

                var quizEnrolment = new QuizEnrolment() { UserId = db.Users.FirstOrDefault(u => u.UserName == "pesho@pesho.com").Id,
                    PastEnglishStudyingDescription = "PastEnglishStudyingDescription text",
                    EnglishGoalsDescription = "EnglishGoalsDescription text",
                    DepartmentId = 1,
                    CityId = 231,
                    CambridgeCertificate = 1,
                    CambridgeCertificateDescription = "CambridgeCertificate text",
                    CambridgeCertificateGrade = 1,
                    Status = QuizEnrolmentStatus.New,
                    DateCreated = DateTime.Now, TotalPoints = 0, QuizId = quizA0.Id, IsCompleted = false };

                db.QuizEnrolments.AddOrUpdate(quizEnrolment);
                db.SaveChanges();
                //db.QuizResults.AddOrUpdate(new QuizResult() { Answer = answerWithInput, Text = "me", QuizEnrolment = quizEnrolment });
                db.SaveChanges();
            }
        }

        private void AssignProductionConfiguration(BritanicaQuizDbContext db)
        {
            if (!(db.Quizzes.Any()))
            {
                var quizC1 = new Quiz() { Name = "C1", MaximumPoints = 80,
                    ShortDescription = "Абсолютно начинаещо ниво", LongDescription = "Гъвкаво и свободно използвам езика с допълнения, свързващи изречения и други сложни структурни форми, независимо дали е неформален или формален разговор."};
                var quizB2Plus = new Quiz() { Name = "B2+", MaximumPoints = 80,
                    ShortDescription = "Високо ниво", LongDescription = "Нямам затруднения със сложен текст. Мога да използвам свободно прилагателни и наречия и да структурирам гледни точки.", NextQuiz = quizC1 };
                var quizB2 = new Quiz() { Name = "B2", MaximumPoints = 80,
                    ShortDescription = "Добро ниво", LongDescription = "Разбирам сложни изречения. Реагирам в спонтанни ситуации и мога да описвам детайли по познати и непознати теми.", NextQuiz = quizB2Plus };
                var quizB1 = new Quiz() { Name = "B1", MaximumPoints = 80,
                    ShortDescription = "Ограничено ниво", LongDescription = "Ориентирам се в езика, когато ми се налага да го използвам в непозната среда - като турист или в дискусия за абстрактни езикови моменти като разговор за мечти, събития и преживявания.", NextQuiz = quizB2 };
                var quizA2 = new Quiz() { Name = "A2", MaximumPoints = 80,
                    ShortDescription = "Елементарно ниво", LongDescription = "Използвам основните думи и фрази по добре позната тема и разбирам прости изречения.", NextQuiz = quizB1 };
                var quizA1 = new Quiz() { Name = "A1", MaximumPoints = 40,
                    ShortDescription = "Начинаещо ниво", LongDescription = "Имам бегли познания по английски език, мога да обясня как се казвам и къде живея.", NextQuiz = quizB1 };
                var quizA0 = new Quiz() { Name = "A0", MaximumPoints = 36,
                    ShortDescription = "Абсолютно начинаещо ниво", LongDescription = "Нямам никакви познания по английски език.", NextQuiz = quizA1 };

                db.Quizzes.AddOrUpdate(quizC1, quizB2Plus, quizB2, quizB1, quizA2, quizA1, quizA0);
                db.SaveChanges();

                #region A0

                var questionSetA0Vocabulary1 = new QuestionSet() { Text = "<p>VOCABULARY</p>", SkillType = SkillType.Vocabulary, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA0 };
                var questionSetA0Reading1 = new QuestionSet() { Text = "<p>1 Read the text.Tick (V) True or False.</p> <p>The Beatles</p> <p>The Beatles were an English rock band from the city of Liverpool.The band had four members: John Lennon, Paul McCartney, George Harrison, and Ringo Starr. The Beatles became one of the biggest rock bands in the world in the 1960s.</p> <p>John Lennon started a band with several of his friends in March 1957 when he was only 16 years old. It wasn’t called the Beatles at that time. Paul McCartney joined the band in the same year when he was 15 years old. In February 1958, McCartney invited his friend, George Harrison, to come and watch them play in a club. Not long after that, Harrison joined the band, too. Both McCartney and Harrison played guitars. In 1962, Ringo Starr became the last person to join the Beatles. Starr played the drums. He got some drums for his birthday when he was a child and that’s how he became interested in playing them.</p> <p>In the early days, the band played in lots of small bars and clubs in Liverpooland their first hit was a song called <i>Love Me Do</i>. The band quickly became popular in the UK and, by 1964, they were famous all over the world.</p> <p>They met their manager, a man called Brian Epstein, at the Cavern Club in Liverpool. Epstein was very interested in the band and immediately became good friends with them. He started work as their manager in 1961. Epstein later said, “I immediately liked what I heard. They were fresh, and they were honest...a star quality.” The Beatles recorded their first album at the Abbey Road Studios in London. It was called <i>Please Please Me</i> and every song on the album went to number one.</p> <p>During the 1960s, the Beatles went on tour several times. Some of the countries they visited were the USA, Denmark, Hong Kong, New Zealand, and Australia. The band was popular until1970, when they stopped making music together. Now – morethan 50 years later –people still love their music and their <i>Greatest Hits</i> album sold 13 million copies in a month.</p>", SkillType = SkillType.Reading, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA0 };
                var questionSetA0Grammar1 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA0 };
                var questionSetA0Grammar2 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA0 };
                var questionSetA0Grammar3 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA0 };
                var questionSetA0Grammar4 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA0 };
                var questionSetA0Grammar5 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA0 };
                var questionSetA0Grammar6 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA0 };
                var questionSetA0Grammar7 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA0 };
                var questionSetA0Grammar8 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA0 };
                var questionSetA0Grammar9 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA0 };
                var questionSetA0Grammar11 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA0 };
                var questionSetA0Grammar12 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA0 };
                var questionSetA0Grammar13 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA0 };
                var questionSetA0Grammar10 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA0 };

                db.QuestionSets.Add(questionSetA0Vocabulary1);
                db.QuestionSets.Add(questionSetA0Reading1);
                db.QuestionSets.Add(questionSetA0Grammar2);
                db.QuestionSets.Add(questionSetA0Grammar3);
                db.QuestionSets.Add(questionSetA0Grammar4);
                db.QuestionSets.Add(questionSetA0Grammar5);
                db.QuestionSets.Add(questionSetA0Grammar6);
                db.QuestionSets.Add(questionSetA0Grammar7);
                db.QuestionSets.Add(questionSetA0Grammar8);
                db.QuestionSets.Add(questionSetA0Grammar9);
                db.QuestionSets.Add(questionSetA0Grammar10);
                db.QuestionSets.Add(questionSetA0Grammar11);
                db.QuestionSets.Add(questionSetA0Grammar12);
                db.QuestionSets.Add(questionSetA0Grammar13);

                db.SaveChanges();

                var questionA0Vocabulary1 = new InputAnswerQuestion() { Text = @"<p> Write the missing word. </p> <p> Example: 0 seven – seventh, eight – <em><u>eighth</u></em> </p> <p> 1 one – first, two – [] </p> <p> 2 slow – fast, cheap – [] </p> <p> 3 husband – wife, son – [] </p> <p> 4 shop assistant – shop, nurse– [] </p> <p> 5 November – December, January – [] </p> <p> 6 child – children, woman – [] </p> <p> 7 Monday -Tuesday, Wednesday – [] </p>", QuestionSet = questionSetA0Vocabulary1 };
                var questionA0Reading1 = new OneAnswerQuestion() { Text = "1 The Beatles didn’t come from London. <p>[] []</p>", QuestionSet = questionSetA0Reading1 };
                var questionA0Reading2 = new OneAnswerQuestion() { Text = "2 George Harrison was a friend of John Lennon before he joined the Beatles. <p>[] []</p>", QuestionSet = questionSetA0Reading1 };
                var questionA0Reading3 = new OneAnswerQuestion() { Text = "3 Ringo Starr started playing the drums when he was a teenager. <p>[] []</p>", QuestionSet = questionSetA0Reading1 };
                var questionA0Reading4 = new OneAnswerQuestion() { Text = "4 The Beatles played in small places before they were famous. <p>[] []</p>", QuestionSet = questionSetA0Reading1 };
                var questionA0Reading5 = new OneAnswerQuestion() { Text = "5 All the songs on the Beatles’ first album went to number one. <p>[] []</p>", QuestionSet = questionSetA0Reading1 };
                var questionA0Reading6 = new InputAnswerQuestion() { Text = "<p>Read the text again and complete the sentences with a word or a number from the text.</p> <p>Example: The Beatles were one of the world’s most famous <i>rock</i> bands.</p> <p>1 Paul McCartney was [] years old when he started to work with John Lennon.</p> <p>2 In February 1958, George Harrison went to see Lennon and McCartney [] in a club.</p> <p>3 Ringo Starr was the fourth [] to join the Beatles in 1962.</p> <p>4 Brian Epstein became the Beatles’ [] in 1961</p> <p>5 The Beatles [] working together in 1970.</p>", QuestionSet = questionSetA0Reading1 };
                var questionA0Grammar1 = new OneAnswerQuestion() { Text = "Where _____ Steve from? <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA0Grammar1 };
                var questionA0Grammar2 = new OneAnswerQuestion() { Text = "I _____ 20 years old. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA0Grammar2 };
                var questionA0Grammar3 = new OneAnswerQuestion() { Text = "_____ a problem with our reservation. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA0Grammar3 };
                var questionA0Grammar4 = new OneAnswerQuestion() { Text = "This is my aunt. _____ name is Joanna. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA0Grammar4 };
                var questionA0Grammar5 = new OneAnswerQuestion() { Text = "Mark is <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA0Grammar5 };
                var questionA0Grammar6 = new OneAnswerQuestion() { Text = "He lives in _____ small town. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA0Grammar6 };
                var questionA0Grammar7 = new OneAnswerQuestion() { Text = "_____ Mark at the office last Friday? <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA0Grammar7 };
                var questionA0Grammar8 = new OneAnswerQuestion() { Text = "Do you like her new shoes? She bought _____ yesterday. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA0Grammar8 };
                var questionA0Grammar9 = new OneAnswerQuestion() { Text = "_____ any people in the shop now. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA0Grammar9 };
                var questionA0Grammar10 = new OneAnswerQuestion() { Text = "_____ I'm sorry, but you _____ park here. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA0Grammar10 };
                var questionA0Grammar11 = new OneAnswerQuestion() { Text = "Do you like _____ ? <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA0Grammar11 };
                var questionA0Grammar12 = new OneAnswerQuestion() { Text = "Where _____ your son live now? <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA0Grammar12 };
                var questionA0Grammar13 = new OneAnswerQuestion() { Text = "Where _____ you go to school when you were a child ? <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA0Grammar13 };


                db.Questions.Add(questionA0Vocabulary1);
                db.Questions.Add(questionA0Reading1);
                db.Questions.Add(questionA0Reading2);
                db.Questions.Add(questionA0Reading3);
                db.Questions.Add(questionA0Reading4);
                db.Questions.Add(questionA0Reading5);
                db.Questions.Add(questionA0Reading6);
                db.Questions.Add(questionA0Grammar1);
                db.Questions.Add(questionA0Grammar2);
                db.Questions.Add(questionA0Grammar3);
                db.Questions.Add(questionA0Grammar4);
                db.Questions.Add(questionA0Grammar5);
                db.Questions.Add(questionA0Grammar6);
                db.Questions.Add(questionA0Grammar7);
                db.Questions.Add(questionA0Grammar8);
                db.Questions.Add(questionA0Grammar9);
                db.Questions.Add(questionA0Grammar10);
                db.Questions.Add(questionA0Grammar11);
                db.Questions.Add(questionA0Grammar12);
                db.Questions.Add(questionA0Grammar13);

                db.SaveChanges();

                // Answers
                // questionA0Vocabulary1
                db.Answers.Add(new Answer() { Text = "three", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Vocabulary1 });
                db.Answers.Add(new Answer() { Text = "expensive", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Vocabulary1 });
                db.Answers.Add(new Answer() { Text = "daughter", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Vocabulary1 });
                db.Answers.Add(new Answer() { Text = "hospital", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Vocabulary1 });
                db.Answers.Add(new Answer() { Text = "February", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Vocabulary1 });
                db.Answers.Add(new Answer() { Text = "women", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Vocabulary1 });
                db.Answers.Add(new Answer() { Text = "Thursday", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Vocabulary1 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Vocabulary1 });

                // questionA0Reading1
                db.Answers.Add(new Answer() { Text = "True", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Reading1 });
                db.Answers.Add(new Answer() { Text = "False", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Reading1 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Reading1 });

                // questionA0Reading2
                db.Answers.Add(new Answer() { Text = "True", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Reading2 });
                db.Answers.Add(new Answer() { Text = "False", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Reading2 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Reading2 });

                // questionA0Reading3
                db.Answers.Add(new Answer() { Text = "True", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Reading3 });
                db.Answers.Add(new Answer() { Text = "False", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Reading3 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Reading3 });

                // questionA0Reading4
                db.Answers.Add(new Answer() { Text = "True", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Reading4 });
                db.Answers.Add(new Answer() { Text = "False", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Reading4 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Reading4 });

                // questionA0Reading5
                db.Answers.Add(new Answer() { Text = "True", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Reading5 });
                db.Answers.Add(new Answer() { Text = "False", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Reading5 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Reading5 });

                // questionA0Reading6

                db.Answers.Add(new Answer() { Text = "fifteen|15", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Reading6 });
                db.Answers.Add(new Answer() { Text = "play", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Reading6 });
                db.Answers.Add(new Answer() { Text = "person", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Reading6 });
                db.Answers.Add(new Answer() { Text = "manager", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Reading6 });
                db.Answers.Add(new Answer() { Text = "stopped", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Reading6 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA0Reading6 });

                // questionA0Grammar1
                db.Answers.Add(new Answer() { Text = "is he", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar1 });
                db.Answers.Add(new Answer() { Text = "are", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Grammar1 });
                db.Answers.Add(new Answer() { Text = "is", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar1 });
                db.Answers.Add(new Answer() { Text = "he is", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar1 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar1 });

                // questionA0Grammar2
                db.Answers.Add(new Answer() { Text = "are", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar2 });
                db.Answers.Add(new Answer() { Text = "is", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Grammar2 });
                db.Answers.Add(new Answer() { Text = "am", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar2 });
                db.Answers.Add(new Answer() { Text = "be", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar2 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar2 });

                //db.Questions.Add(questionA0Grammar3);
                db.Answers.Add(new Answer() { Text = "This is", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar3 });
                db.Answers.Add(new Answer() { Text = "There is", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Grammar3 });
                db.Answers.Add(new Answer() { Text = "They are", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar3 });
                db.Answers.Add(new Answer() { Text = "There are", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar3 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar3 });

                //db.Questions.Add(questionA0Grammar4);
                db.Answers.Add(new Answer() { Text = "Her", IsCorrect = true, Points = 0, NegativePoints = 0, Question = questionA0Grammar4 });
                db.Answers.Add(new Answer() { Text = "His", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA0Grammar4 });
                db.Answers.Add(new Answer() { Text = "Yours", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar4 });
                db.Answers.Add(new Answer() { Text = "Our", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar4 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar4 });

                //db.Questions.Add(questionA0Grammar5);
                db.Answers.Add(new Answer() { Text = "my sister’s husband", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Grammar5 });
                db.Answers.Add(new Answer() { Text = "husband my sister", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar5 });
                db.Answers.Add(new Answer() { Text = "husband from my sister", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar5 });
                db.Answers.Add(new Answer() { Text = "my sister  husband’s", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar5 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar5 });

                //db.Questions.Add(questionA0Grammar6);
                db.Answers.Add(new Answer() { Text = "the", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar6 });
                db.Answers.Add(new Answer() { Text = "an", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar6 });
                db.Answers.Add(new Answer() { Text = "a", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Grammar6 });
                db.Answers.Add(new Answer() { Text = string.Empty, IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar6 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar6 });

                //db.Questions.Add(questionA0Grammar7);
                db.Answers.Add(new Answer() { Text = "Was", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Grammar7 });
                db.Answers.Add(new Answer() { Text = "Were", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar7 });
                db.Answers.Add(new Answer() { Text = "Does", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar7 });
                db.Answers.Add(new Answer() { Text = "Did", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar7 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar7 });

                //db.Questions.Add(questionA0Grammar8);
                db.Answers.Add(new Answer() { Text = "it", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar8 });
                db.Answers.Add(new Answer() { Text = "they", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar8 });
                db.Answers.Add(new Answer() { Text = "them", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Grammar8 });
                db.Answers.Add(new Answer() { Text = "their", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar8 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar8 });

                //db.Questions.Add(questionA0Grammar9);
                db.Answers.Add(new Answer() { Text = "There are", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar9 });
                db.Answers.Add(new Answer() { Text = "There isn't", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar9 });
                db.Answers.Add(new Answer() { Text = "There were", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar9 });
                db.Answers.Add(new Answer() { Text = "There aren't", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Grammar9 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar9 });

                //db.Questions.Add(questionA0Grammar10);
                db.Answers.Add(new Answer() { Text = "can't", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Grammar10 });
                db.Answers.Add(new Answer() { Text = "are", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar10 });
                db.Answers.Add(new Answer() { Text = "can", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar10 });
                db.Answers.Add(new Answer() { Text = "not", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar10 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar10 });

                //db.Questions.Add(questionA0Grammar11);
                db.Answers.Add(new Answer() { Text = "swim", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar11 });
                db.Answers.Add(new Answer() { Text = "swimming", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Grammar11 });
                db.Answers.Add(new Answer() { Text = "swiming", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar11 });
                db.Answers.Add(new Answer() { Text = "to swim", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar11 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar11 });

                //db.Questions.Add(questionA0Grammar12);
                db.Answers.Add(new Answer() { Text = "-", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar12 });
                db.Answers.Add(new Answer() { Text = "do", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar12 });
                db.Answers.Add(new Answer() { Text = "are", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar12 });
                db.Answers.Add(new Answer() { Text = "does", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Grammar12 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar12 });

                //db.Questions.Add(questionA0Grammar13);
                db.Answers.Add(new Answer() { Text = "does", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar13 });
                db.Answers.Add(new Answer() { Text = "did", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA0Grammar13 });
                db.Answers.Add(new Answer() { Text = "were", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar13 });
                db.Answers.Add(new Answer() { Text = "are", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar13 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA0Grammar13 });

                db.SaveChanges();

                #endregion

                #region A1

                var questionSetA1Vocabulary1 = new QuestionSet() { Text = @"<p>VOCABULARY</p><p>In each line, Tick (v) one word which does NOT belong to the group.</p> Example: mushrooms onions <b>apples v</b> potatoes</p>", SkillType = SkillType.Vocabulary, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA1 };
                var questionSetA1Vocabulary2 = new QuestionSet() { SkillType = SkillType.Vocabulary, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA1 };
                var questionSetA1Reading1 = new QuestionSet() { Text = @"<p>READING</p> <p>1 Read the text and tick (V) A, B, or C.</p> <p>The best place in the world to live</p> <p><i>Vancouver is the third biggest city in Canada. It’s in the south west of the country and it has a population of 2.6 million. A recent study showed that it’s the best city in the world to live. The study looked at areas like weather, transport, education, healthcare, and safety. This study happens every year and Vancouver is usually number one.</i></p> <p><i>We asked Vancouver resident Jacob Meyers if he agrees.</i></p> <p>I’ve lived in Vancouver all my life. I work for an engineering company and I’ve travelled on business to many Canadian cities. Two years ago another company offered me a very good job in Montreal but I didn’t take it because I never want to leave my city. Let me tell you why not:</p> <p>Vancouver is situated between the mountains and the Pacific Ocean. That means the summers aren’t too hot and the winters aren’t too cold. It also rains a lot, in summer and winter. Some people don’t like that but I do because our gardens and parks are always green and fresh. Every day I cycle or walk for an hour in a park and in Vancouver you’re never more than a few minutes from one.</p> <p>I don’t feel frightened when I walk in Vancouver’s streets at night. Of course, Vancouver has a high population and there’s crime in every big city, but compared to the USA, for example, it’s quite safe.</p> <p>But my favourite thing about Vancouver is its incredible mix of nationalities. I’ve got friends here from all over the world. And there’s an enormous variety of foreign food in the restaurants and markets. I’ve eaten wonderful dishes from China, India, Italy, Greece, and Japan and I haven’t been to any of these places.</p> <p>Example: Vancouver is bigger than all the other Canadian cities.</p> <p>A True <b>B False</b> v C Doesn’t say</p>", SkillType = SkillType.Reading, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA1 };
                var questionSetA1Grammar1 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA1 };
                var questionSetA1Grammar2 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA1 };
                var questionSetA1Grammar3 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA1 };
                var questionSetA1Grammar4 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA1 };
                var questionSetA1Grammar5 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA1 };
                var questionSetA1Grammar6 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA1 };
                var questionSetA1Grammar7 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA1 };
                var questionSetA1Grammar8 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA1 };
                var questionSetA1Grammar9 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA1 };
                var questionSetA1Grammar10 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA1 };
                var questionSetA1Grammar11 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA1 };
                var questionSetA1Grammar12 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA1 };
                var questionSetA1Grammar13 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA1 };
                var questionSetA1Grammar14 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA1 };
                var questionSetA1Grammar15 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA1 };

                db.QuestionSets.Add(questionSetA1Vocabulary1);
                db.QuestionSets.Add(questionSetA1Vocabulary2);
                db.QuestionSets.Add(questionSetA1Reading1);
                db.QuestionSets.Add(questionSetA1Grammar1);
                db.QuestionSets.Add(questionSetA1Grammar2);
                db.QuestionSets.Add(questionSetA1Grammar3);
                db.QuestionSets.Add(questionSetA1Grammar4);
                db.QuestionSets.Add(questionSetA1Grammar5);
                db.QuestionSets.Add(questionSetA1Grammar6);
                db.QuestionSets.Add(questionSetA1Grammar7);
                db.QuestionSets.Add(questionSetA1Grammar8);
                db.QuestionSets.Add(questionSetA1Grammar9);
                db.QuestionSets.Add(questionSetA1Grammar10);
                db.QuestionSets.Add(questionSetA1Grammar11);
                db.QuestionSets.Add(questionSetA1Grammar12);
                db.QuestionSets.Add(questionSetA1Grammar13);
                db.QuestionSets.Add(questionSetA1Grammar14);
                db.QuestionSets.Add(questionSetA1Grammar15);
                db.SaveChanges();

                var questionA1Vocabulary1 = new OneAnswerQuestion() { Text = "<p>1 [] [] [] []</p>", QuestionSet = questionSetA1Vocabulary1 };
                var questionA1Vocabulary2 = new OneAnswerQuestion() { Text = "<p>2 [] [] [] []</p>", QuestionSet = questionSetA1Vocabulary1 };
                var questionA1Vocabulary3 = new OneAnswerQuestion() { Text = "<p>3 [] [] [] []</p>", QuestionSet = questionSetA1Vocabulary1 };
                var questionA1Vocabulary4 = new OneAnswerQuestion() { Text = "<p>4 [] [] [] []</p>", QuestionSet = questionSetA1Vocabulary1 };
                var questionA1Vocabulary5 = new OneAnswerQuestion() { Text = "<p>5 [] [] [] []</p>", QuestionSet = questionSetA1Vocabulary1 };

                var questionA1Vocabulary6 = new InputAnswerQuestion() { Text = "<p>2. What is the next word?</p> <p>Example: 0. one, two, <i>three</i></p> <p>1 Sunday, Monday, []</p> <p>2 twenty, thirty, []</p> <p>3 July, August, []</p> <p>4 summer, autumn, []</p> <p>5 third, fourth, []</p>", QuestionSet = questionSetA1Vocabulary2 };

                var questionA1Reading1 = new OneAnswerQuestion() { Text = "<p>1 Jacob was born in Vancouver.</p><p>[] [] []</p>", QuestionSet = questionSetA1Reading1 };
                var questionA1Reading2 = new OneAnswerQuestion() { Text = "<p>2 There are some mountains between Vancouver and the ocean.</p><p>[] [] []</p>", QuestionSet = questionSetA1Reading1 };
                var questionA1Reading3 = new OneAnswerQuestion() { Text = "<p>3 Jacob has a big garden.</p><p>[] [] []</p>", QuestionSet = questionSetA1Reading1 };
                var questionA1Reading4 = new OneAnswerQuestion() { Text = "<p>4 He never walks in the streets at night.</p><p>[] [] []</p>", QuestionSet = questionSetA1Reading1 };
                var questionA1Reading5 = new OneAnswerQuestion() { Text = "<p>5 Jacob can speak a lot of foreign languages.</p><p>[] [] []</p>", QuestionSet = questionSetA1Reading1 };

                var questionA1Grammar1 = new OneAnswerQuestion() { Text = "Simon ________________ Chinese food. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA1Grammar1 };
                var questionA1Grammar2 = new OneAnswerQuestion() { Text = "Where________________  come from? <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA1Grammar2 };
                var questionA1Grammar3 = new OneAnswerQuestion() { Text = "Alex _____________ to school by bus. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA1Grammar3 };
                var questionA1Grammar4 = new OneAnswerQuestion() { Text = "Sorry, I can’t talk. I _____________ right now. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA1Grammar4 };
                var questionA1Grammar5 = new OneAnswerQuestion() { Text = "She _________________ at university in 2003. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA1Grammar5 };
                var questionA1Grammar6 = new OneAnswerQuestion() { Text = "I _________________ a very interesting film last Sunday. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA1Grammar6 };
                var questionA1Grammar7 = new OneAnswerQuestion() { Text = "The restaurant is _________________ to the cinema.<p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA1Grammar7 };
                var questionA1Grammar8 = new OneAnswerQuestion() { Text = "They left _________________ Monday morning. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA1Grammar8 };
                var questionA1Grammar9 = new OneAnswerQuestion() { Text = "We need to go shopping today. There isn’t ________________ food in the fridge. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA1Grammar9 };
                var questionA1Grammar10 = new OneAnswerQuestion() { Text = "We would like to have ________________ information about your services. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA1Grammar10 };
                var questionA1Grammar11 = new OneAnswerQuestion() { Text = "He is ___________________ all the other kids in his class. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA1Grammar11 };
                var questionA1Grammar12 = new OneAnswerQuestion() { Text = "We are trying to save money as we ____________________buy a new car. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA1Grammar12 };
                var questionA1Grammar13 = new OneAnswerQuestion() { Text = "________________ eaten sushi before? <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA1Grammar13 };
                var questionA1Grammar14 = new OneAnswerQuestion() { Text = "I usually _________________ a bike to work. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA1Grammar14 };
                var questionA1Grammar15 = new OneAnswerQuestion() { Text = "I _________________my old friend Jack at the station yesterday. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA1Grammar15 };

                db.Questions.Add(questionA1Vocabulary1);
                db.Questions.Add(questionA1Vocabulary2);
                db.Questions.Add(questionA1Vocabulary3);
                db.Questions.Add(questionA1Vocabulary4);
                db.Questions.Add(questionA1Vocabulary5);
                db.Questions.Add(questionA1Vocabulary6);
                db.Questions.Add(questionA1Reading1);
                db.Questions.Add(questionA1Reading2);
                db.Questions.Add(questionA1Reading3);
                db.Questions.Add(questionA1Reading4);
                db.Questions.Add(questionA1Reading5);
                db.Questions.Add(questionA1Grammar1);
                db.Questions.Add(questionA1Grammar2);
                db.Questions.Add(questionA1Grammar3);
                db.Questions.Add(questionA1Grammar4);
                db.Questions.Add(questionA1Grammar5);
                db.Questions.Add(questionA1Grammar6);
                db.Questions.Add(questionA1Grammar7);
                db.Questions.Add(questionA1Grammar8);
                db.Questions.Add(questionA1Grammar9);
                db.Questions.Add(questionA1Grammar10);
                db.Questions.Add(questionA1Grammar11);
                db.Questions.Add(questionA1Grammar12);
                db.Questions.Add(questionA1Grammar13);
                db.Questions.Add(questionA1Grammar14);
                db.Questions.Add(questionA1Grammar15);
                db.SaveChanges();

                // questionSetA1Vocabulary1
                db.Answers.Add(new Answer() { Text = "carefully", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary1 });
                db.Answers.Add(new Answer() { Text = "serious", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary1 });
                db.Answers.Add(new Answer() { Text = "slowly", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary1 });
                db.Answers.Add(new Answer() { Text = "fast", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary1 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary1 });

                db.Answers.Add(new Answer() { Text = "pineaplles", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary2 });
                db.Answers.Add(new Answer() { Text = "bananas", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary2 });
                db.Answers.Add(new Answer() { Text = "carrots", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary2 });
                db.Answers.Add(new Answer() { Text = "grapes", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary2 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary2 });

                db.Answers.Add(new Answer() { Text = "fireplace", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary3 });
                db.Answers.Add(new Answer() { Text = "square", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary3 });
                db.Answers.Add(new Answer() { Text = "road", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary3 });
                db.Answers.Add(new Answer() { Text = "bridge", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary3 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary3 });

                db.Answers.Add(new Answer() { Text = "fridge", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary4 });
                db.Answers.Add(new Answer() { Text = "cupboard", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary4 });
                db.Answers.Add(new Answer() { Text = "mirror", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary4 });
                db.Answers.Add(new Answer() { Text = "ceiling", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary4 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary4 });

                db.Answers.Add(new Answer() { Text = "opposite", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary5 });
                db.Answers.Add(new Answer() { Text = "there", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary5 });
                db.Answers.Add(new Answer() { Text = "under", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary5 });
                db.Answers.Add(new Answer() { Text = "behind", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary5 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary5 });

                //questionSetA1Vocabulary2
                db.Answers.Add(new Answer() { Text = "Tuesday", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary6 });
                db.Answers.Add(new Answer() { Text = "forty", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary6 });
                db.Answers.Add(new Answer() { Text = "September", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary6 });
                db.Answers.Add(new Answer() { Text = "winter", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary6 });
                db.Answers.Add(new Answer() { Text = "fifth", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary6 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Vocabulary6 });

                //questionSetA1ReadingAll

                db.Answers.Add(new Answer() { Text = "True", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Reading1 });
                db.Answers.Add(new Answer() { Text = "False", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Reading1 });
                db.Answers.Add(new Answer() { Text = "No information", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Reading1 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Reading1 });

                db.Answers.Add(new Answer() { Text = "True", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Reading2 });
                db.Answers.Add(new Answer() { Text = "False", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Reading2 });
                db.Answers.Add(new Answer() { Text = "No information", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Reading2 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Reading2 });

                db.Answers.Add(new Answer() { Text = "True", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Reading3 });
                db.Answers.Add(new Answer() { Text = "False", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Reading3 });
                db.Answers.Add(new Answer() { Text = "No information", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Reading3 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Reading3 });

                db.Answers.Add(new Answer() { Text = "True", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Reading4 });
                db.Answers.Add(new Answer() { Text = "False", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Reading4 });
                db.Answers.Add(new Answer() { Text = "No information", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Reading4 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Reading4 });

                db.Answers.Add(new Answer() { Text = "True", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Reading5 });
                db.Answers.Add(new Answer() { Text = "False", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Reading5 });
                db.Answers.Add(new Answer() { Text = "No information", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Reading5 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Reading5 });

                // questionA1Grammar1
                db.Answers.Add(new Answer() { Text = "likes not", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar1 });
                db.Answers.Add(new Answer() { Text = "don't like", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar1 });
                db.Answers.Add(new Answer() { Text = "doesn't like", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Grammar1 });
                db.Answers.Add(new Answer() { Text = "isn't likes", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar1 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar1 });

                // questionA1Grammar2
                db.Answers.Add(new Answer() { Text = "are you", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar2 });
                db.Answers.Add(new Answer() { Text = "do you", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Grammar2 });
                db.Answers.Add(new Answer() { Text = "you", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar2 });
                db.Answers.Add(new Answer() { Text = "you are", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar2 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar2 });

                // questionA1Grammar3
                db.Answers.Add(new Answer() { Text = "goes usually", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar3 });
                db.Answers.Add(new Answer() { Text = "go usually", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar3 });
                db.Answers.Add(new Answer() { Text = "usually go", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar3 });
                db.Answers.Add(new Answer() { Text = "usually goes", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Grammar3 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar3 });

                // questionA1Grammar4
                db.Answers.Add(new Answer() { Text = "'m driving", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Grammar4 });
                db.Answers.Add(new Answer() { Text = "drives", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar4 });
                db.Answers.Add(new Answer() { Text = "driving", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar4 });
                db.Answers.Add(new Answer() { Text = "drive", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar4 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar4 });

                // questionA1Grammar5
                db.Answers.Add(new Answer() { Text = "didn't be", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Grammar5 });
                db.Answers.Add(new Answer() { Text = "weren't", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar5 });
                db.Answers.Add(new Answer() { Text = "wasn't", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar5 });
                db.Answers.Add(new Answer() { Text = "isn't", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar5 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar5 });

                // questionA1Grammar6
                db.Answers.Add(new Answer() { Text = "watch", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar6 });
                db.Answers.Add(new Answer() { Text = "watches", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar6 });
                db.Answers.Add(new Answer() { Text = "watched", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Grammar6 });
                db.Answers.Add(new Answer() { Text = "watching", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar6 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar6 });

                // questionA1Grammar7
                db.Answers.Add(new Answer() { Text = "next", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Grammar7 });
                db.Answers.Add(new Answer() { Text = "opposite", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar7 });
                db.Answers.Add(new Answer() { Text = "behind", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar7 });
                db.Answers.Add(new Answer() { Text = "in front", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar7 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar7 });

                // questionA1Grammar8
                db.Answers.Add(new Answer() { Text = "in", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar8 });
                db.Answers.Add(new Answer() { Text = "on", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Grammar8 });
                db.Answers.Add(new Answer() { Text = "at", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar8 });
                db.Answers.Add(new Answer() { Text = "from", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar8 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar8 });

                // questionA1Grammar9
                db.Answers.Add(new Answer() { Text = "some", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar9 });
                db.Answers.Add(new Answer() { Text = "any", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Grammar9 });
                db.Answers.Add(new Answer() { Text = "no", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar9 });
                db.Answers.Add(new Answer() { Text = "a", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar9 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar9 });

                // questionA1Grammar10
                db.Answers.Add(new Answer() { Text = "some", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Grammar10 });
                db.Answers.Add(new Answer() { Text = "any", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar10 });
                db.Answers.Add(new Answer() { Text = "an", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar10 });
                db.Answers.Add(new Answer() { Text = "a", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar10 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar10 });

                // questionA1Grammar11
                db.Answers.Add(new Answer() { Text = "taller that", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar11 });
                db.Answers.Add(new Answer() { Text = "more tall that", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar11 });
                db.Answers.Add(new Answer() { Text = "more taller than", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar11 });
                db.Answers.Add(new Answer() { Text = "taller than", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Grammar11 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar11 });

                // questionA1Grammar12
                db.Answers.Add(new Answer() { Text = "will", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar12 });
                db.Answers.Add(new Answer() { Text = "going to", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar12 });
                db.Answers.Add(new Answer() { Text = "are going to", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Grammar12 });
                db.Answers.Add(new Answer() { Text = "will going to", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar12 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar12 });

                // questionA1Grammar13
                db.Answers.Add(new Answer() { Text = "Did you ever", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar13 });
                db.Answers.Add(new Answer() { Text = "Are you ever", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar13 });
                db.Answers.Add(new Answer() { Text = "Have you ever", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Grammar13 });
                db.Answers.Add(new Answer() { Text = "Do you ever", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar13 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar13 });

                // questionA1Grammar14
                db.Answers.Add(new Answer() { Text = "drive", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar14 });
                db.Answers.Add(new Answer() { Text = "ride", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Grammar14 });
                db.Answers.Add(new Answer() { Text = "take", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar14 });
                db.Answers.Add(new Answer() { Text = "cycle", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar14 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar14 });

                // questionA1Grammar15
                db.Answers.Add(new Answer() { Text = "see", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar15 });
                db.Answers.Add(new Answer() { Text = "seen", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar15 });
                db.Answers.Add(new Answer() { Text = "saw", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA1Grammar15 });
                db.Answers.Add(new Answer() { Text = "sat", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar15 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA1Grammar15 });

                db.SaveChanges();

                #endregion

                // unfinished
                #region A2

                var questionSetA2Vocabulary1= new QuestionSet() { SkillType = SkillType.Vocabulary, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };
                var questionSetA2Vocabulary2= new QuestionSet() { SkillType = SkillType.Vocabulary, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };
                var questionSetA2Vocabulary3= new QuestionSet() { SkillType = SkillType.Vocabulary, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };
                var questionSetA2Vocabulary4= new QuestionSet() { SkillType = SkillType.Vocabulary, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };
                var questionSetA2Vocabulary5= new QuestionSet() { SkillType = SkillType.Vocabulary, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };
                var questionSetA2Vocabulary6= new QuestionSet() { SkillType = SkillType.Vocabulary, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };
                var questionSetA2Vocabulary7= new QuestionSet() { SkillType = SkillType.Vocabulary, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };
                var questionSetA2Vocabulary8= new QuestionSet() { SkillType = SkillType.Vocabulary, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };
                var questionSetA2Vocabulary9= new QuestionSet() { SkillType = SkillType.Vocabulary, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };
                var questionSetA2Vocabulary10 = new QuestionSet() { SkillType = SkillType.Vocabulary, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };
                var questionSetA2Vocabulary11 = new QuestionSet() { SkillType = SkillType.Vocabulary, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };
                var questionSetA2Vocabulary12 = new QuestionSet() { SkillType = SkillType.Vocabulary, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };
                var questionSetA2Vocabulary13 = new QuestionSet() { SkillType = SkillType.Vocabulary, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };

                var questionA2Vocabulary1 = new OneAnswerQuestion() { Text = "You need to  _________________your health more.<p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Vocabulary1 };
                var questionA2Vocabulary2 = new OneAnswerQuestion() { Text = "_________________a jacket. It's cold outside.<p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Vocabulary2 };
                var questionA2Vocabulary3 = new OneAnswerQuestion() { Text = "Hello, could I ___________________ to Sue, please?<p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Vocabulary3 };
                var questionA2Vocabulary4 = new OneAnswerQuestion() { Text = "We __________ a really good time at the festival.<p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Vocabulary4 };
                var questionA2Vocabulary5 = new OneAnswerQuestion() { Text = "My brother is my aunt’s __________.<p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Vocabulary5 };
                var questionA2Vocabulary6 = new OneAnswerQuestion() { Text = "Put your lights on. We’re going to drive __________ a tunnel!<p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Vocabulary6 };
                var questionA2Vocabulary7 = new OneAnswerQuestion() { Text = "The opposite of crowded is __________.<p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Vocabulary7 };
                var questionA2Vocabulary8 = new OneAnswerQuestion() { Text = "Kate’s really __________. She always gives me presents.<p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Vocabulary8 };
                var questionA2Vocabulary9 = new OneAnswerQuestion() { Text = "Those jeans look nice. Would you like to __________ them on?<p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Vocabulary9 };
                var questionA2Vocabulary10 = new OneAnswerQuestion() { Text = "Enter our competition now! You could __________ a great prize.<p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Vocabulary10 };
                var questionA2Vocabulary11 = new OneAnswerQuestion() { Text = "Our new school year starts __________ 5th September.<p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Vocabulary11 };
                var questionA2Vocabulary12 = new OneAnswerQuestion() { Text = "We haven’t got much money __________ we aren’t going to buy the flat.<p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Vocabulary12 };
                var questionA2Vocabulary13 = new OneAnswerQuestion() { Text = "He __________ on really well with his sister.<p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Vocabulary13 };

                var questionSetA2Grammar1 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };
                var questionA2grammar1 = new OneAnswerQuestion() { Text = "We’ve known each other____________________ many years.<p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Grammar1 };

                var questionSetA2Grammar2 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };
                var questionA2grammar2 = new OneAnswerQuestion() { Text = "This town ___________________ by lots of tourists during the summer.<p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Grammar2 };

                var questionSetA2Grammar3 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };
                var questionA2grammar3 = new OneAnswerQuestion() { Text = "He_________________ in the ocean when he saw a shark.	<p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Grammar3 };

                var questionSetA2Grammar4 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };
                var questionA2grammar4 = new OneAnswerQuestion() { Text = "If  he has some spare time on the last day of his stay, he _______________ the cathedral.<p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Grammar4 };

                var questionSetA2Grammar5 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };
                var questionA2grammar5 = new OneAnswerQuestion() { Text = "I believe his performance was___________________ that of the professional actors in the musical.<p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Grammar5 };

                var questionSetA2Grammar6 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };
                var questionA2grammar6 = new OneAnswerQuestion() { Text = "When she got home, she realised she _________________ her keys at the office.<p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Grammar6 };

                var questionSetA2Grammar7 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };
                var questionA2grammar7 = new OneAnswerQuestion() { Text = "You _________________ pay anything for the tickets. They’re free.<p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Grammar7 };

                var questionSetA2Grammar8 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };
                var questionA2grammar8 = new OneAnswerQuestion() { Text = "That's the woman ________________ won the lottery last year.<p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Grammar8 };

                var questionSetA2Grammar9 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };
                var questionA2grammar9 = new OneAnswerQuestion() { Text = "___________________ to Italy before?<p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Grammar9 };

                var questionSetA2Grammar10 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };
                var questionA2grammar10 = new OneAnswerQuestion() { Text = "___________________ the dentist at 5 o'clock this afternoon. I made an appointment yesterday.<p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Grammar10 };

                var questionSetA2Grammar11 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };
                var questionA2grammar11 = new OneAnswerQuestion() { Text = "Person A: 'I wasn’t happy with the service.' Person B:  '________________.'<p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Grammar11 };

                var questionSetA2Grammar12 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };
                var questionA2grammar12 = new OneAnswerQuestion() { Text = "As a child, he _________________ in Luxemburg for five years but then he and his parents moved to France. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Grammar12 };


                db.QuestionSets.Add(questionSetA2Vocabulary2);
                db.QuestionSets.Add(questionSetA2Vocabulary3);
                db.QuestionSets.Add(questionSetA2Vocabulary4);
                db.QuestionSets.Add(questionSetA2Vocabulary5);
                db.QuestionSets.Add(questionSetA2Vocabulary6);
                db.QuestionSets.Add(questionSetA2Vocabulary7);
                db.QuestionSets.Add(questionSetA2Vocabulary8);
                db.QuestionSets.Add(questionSetA2Vocabulary9);
                db.QuestionSets.Add(questionSetA2Vocabulary10);
                db.QuestionSets.Add(questionSetA2Vocabulary11);
                db.QuestionSets.Add(questionSetA2Vocabulary12);
                db.QuestionSets.Add(questionSetA2Grammar1);
                db.QuestionSets.Add(questionSetA2Grammar2);
                db.QuestionSets.Add(questionSetA2Grammar3);
                db.QuestionSets.Add(questionSetA2Grammar4);
                db.QuestionSets.Add(questionSetA2Grammar5);
                db.QuestionSets.Add(questionSetA2Grammar6);
                db.QuestionSets.Add(questionSetA2Grammar7);
                db.QuestionSets.Add(questionSetA2Grammar8);
                db.QuestionSets.Add(questionSetA2Grammar9);
                db.QuestionSets.Add(questionSetA2Grammar10);
                db.QuestionSets.Add(questionSetA2Grammar11);
                db.QuestionSets.Add(questionSetA2Grammar12);
                
                db.SaveChanges();

                db.Questions.Add(questionA2grammar1);
                db.Questions.Add(questionA2grammar2);
                db.Questions.Add(questionA2grammar3);
                db.Questions.Add(questionA2grammar4);
                db.Questions.Add(questionA2grammar5);
                db.Questions.Add(questionA2grammar6);
                db.Questions.Add(questionA2grammar7);
                db.Questions.Add(questionA2grammar8);
                db.Questions.Add(questionA2grammar9);
                db.Questions.Add(questionA2grammar10);
                db.Questions.Add(questionA2grammar11);
                db.Questions.Add(questionA2grammar12);
                db.Questions.Add(questionA2Vocabulary13);
                db.Questions.Add(questionA2Vocabulary1);
                db.Questions.Add(questionA2Vocabulary2);
                db.Questions.Add(questionA2Vocabulary3);
                db.Questions.Add(questionA2Vocabulary4);
                db.Questions.Add(questionA2Vocabulary5);
                db.Questions.Add(questionA2Vocabulary6);
                db.Questions.Add(questionA2Vocabulary7);
                db.Questions.Add(questionA2Vocabulary8);
                db.Questions.Add(questionA2Vocabulary9);
                db.Questions.Add(questionA2Vocabulary10);
                db.Questions.Add(questionA2Vocabulary11);
                db.Questions.Add(questionA2Vocabulary12);
                
                db.SaveChanges();
                                
                var questionSetA2Reading1 = new QuestionSet() { Text = @"<p>READING</p> <p> Read the article and tick (v) A, B, or C.</p> <p><i>Couch Surfing </i>– a different kind of travel experience</p> <p><b>by Emma Jackson</b></p> <p>I love travelling abroad, but two years ago I didn’t have much cash to spend on my holiday. At first, I was planning to go camping again, but then a friend suggested an alternative: <i>Couch Surfing</i>. I had no idea what that was, so she explained. <i>‘Couch Surfers’</i> are people who stay as guests in other people’s homes for free, and visit the sights in the local area. You can do the things that most tourists do, like sunbathing on the beach or sightseeing in the town centre. Or your host could give you a language lesson, teach you how to cook local delicacies, or take you to places that visitors never find. It sounded much more fun than my other holidays, so I joined the website and sent emails to about twenty hosts in France and Spain right away.</p> <p>A few days later, I already had ten replies. After a week or two of emails, I made arrangements with four hosts in three different cities. I’m quite talkative and they seemed very friendly, so I wasn’t concerned about spending time with strangers. Two weeks later, I was arriving in Paris, and meeting my first host, Claudette.</p> <p>Over the next ten days, I stayed in four very different homes, improved my foreign languages, and made some great new friends. Sometimes the places where I stayed were basic – a sofa to sleep on, or even just a floor, but sometimes they were luxurious – much nicer than the hostels that I usually go to. I think it was probably the cheapest and most interesting holiday I’ve ever had!</p> <p>I’ve done <i>Couch Surfing</i> again twice since then: in Italy, and here in the UK. I’ll definitely do it again. In fact, I’m going on a trip to South America next year. And I’ve had five visitors at my place, including Claudette. I’ve discovered that hosting is as much fun as exploring a new place. <i>Couch Surfing</i> is a fantastic experience. Try it some time!</p> <p>Example: Emma started <i>Couch Surfing </i>because she wanted to save money.</p> A True v B False C No Information", SkillType = SkillType.Reading, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };
                var questionSetA2Reading2 = new QuestionSet() { SkillType = SkillType.Reading, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizA2 };

                db.QuestionSets.Add(questionSetA2Reading1);
                db.QuestionSets.Add(questionSetA2Reading2);
                db.SaveChanges();

                var questionA2Reading1 = new OneAnswerQuestion() { Text = "<p>1 It was her first holiday in another country.</p><p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Reading1 };
                var questionA2Reading2 = new OneAnswerQuestion() { Text = "<p>2 Emma’s friend was a regular couch surfer.</p><p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Reading1 };
                var questionA2Reading3 = new OneAnswerQuestion() { Text = "<p>3 Two of the people she visited lived in the same city.</p><p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Reading1 };
                var questionA2Reading4 = new OneAnswerQuestion() { Text = "<p>4 Emma went on holiday about a month after joining the website.</p><p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Reading1 };
                var questionA2Reading5 = new OneAnswerQuestion() { Text = "<p>5 Emma is quite shy.</p><p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Reading1 };
                var questionA2Reading6 = new OneAnswerQuestion() { Text = "<p>6 Emma had a nice bed to sleep in at all the places where she stayed.</p><p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetA2Reading1 };
                var questionA2Reading7 = new InputAnswerQuestion() { Text = "<p>Match five of the highlighted words / phrases with the definitions.</p> <p>Example: people who stay in a hotel or house for a short time</p> <p><i>guests</i></p> <p>1 worried [] </p> <p>2 made better []</p> <p>3 finding and learning about something [] </p> <p>4 interesting things for tourists to visit [] </p>", QuestionSet = questionSetA2Reading2 };

                db.Questions.Add(questionA2Reading1);
                db.Questions.Add(questionA2Reading2);
                db.Questions.Add(questionA2Reading3);
                db.Questions.Add(questionA2Reading4);
                db.Questions.Add(questionA2Reading5);
                db.Questions.Add(questionA2Reading6);
                db.Questions.Add(questionA2Reading7);

                db.Answers.Add(new Answer() { Text = "look for", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary1 });
                db.Answers.Add(new Answer() { Text = "look forward", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary1 });
                db.Answers.Add(new Answer() { Text = "look after", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2Vocabulary1 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary1 });

                db.Answers.Add(new Answer() { Text = "put off", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary2 });
                db.Answers.Add(new Answer() { Text = "put on", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2Vocabulary2 });
                db.Answers.Add(new Answer() { Text = "take off", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary2 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary2 });

                db.Answers.Add(new Answer() { Text = "say", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary3 });
                db.Answers.Add(new Answer() { Text = "tell", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary3 });
                db.Answers.Add(new Answer() { Text = "speak", IsCorrect = true, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary3 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary3 });

                db.Answers.Add(new Answer() { Text = "spend", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary4 });
                db.Answers.Add(new Answer() { Text = "did", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary4 });
                db.Answers.Add(new Answer() { Text = "had", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2Vocabulary4 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary4 });

                db.Answers.Add(new Answer() { Text = "niece", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary5 });
                db.Answers.Add(new Answer() { Text = "grandson", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary5 });
                db.Answers.Add(new Answer() { Text = "nephew", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2Vocabulary5 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary5 });

                db.Answers.Add(new Answer() { Text = "through", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2Vocabulary6 });
                db.Answers.Add(new Answer() { Text = "under", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary6 });
                db.Answers.Add(new Answer() { Text = "across", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary6 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary6 });

                db.Answers.Add(new Answer() { Text = "clean", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary7 });
                db.Answers.Add(new Answer() { Text = "safe", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary7 });
                db.Answers.Add(new Answer() { Text = "empty", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2Vocabulary7 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary7 });

                db.Answers.Add(new Answer() { Text = "mean", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary8 });
                db.Answers.Add(new Answer() { Text = "lazy", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary8 });
                db.Answers.Add(new Answer() { Text = "generous", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2Vocabulary8 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary8 });

                db.Answers.Add(new Answer() { Text = "take", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary9 });
                db.Answers.Add(new Answer() { Text = "try", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2Vocabulary9 });
                db.Answers.Add(new Answer() { Text = "wear", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary9 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary9 });

                db.Answers.Add(new Answer() { Text = "win", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2Vocabulary10 });
                db.Answers.Add(new Answer() { Text = "earn", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary10 });
                db.Answers.Add(new Answer() { Text = "make", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary10 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary10 });

                db.Answers.Add(new Answer() { Text = "in", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary11 });
                db.Answers.Add(new Answer() { Text = "on", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2Vocabulary11 });
                db.Answers.Add(new Answer() { Text = "at", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary11 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary11 });

                db.Answers.Add(new Answer() { Text = "because", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary12 });
                db.Answers.Add(new Answer() { Text = "so", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2Vocabulary12 });
                db.Answers.Add(new Answer() { Text = "although", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary12 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary12 });

                db.Answers.Add(new Answer() { Text = "goes", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary13 });
                db.Answers.Add(new Answer() { Text = "gets", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2Vocabulary13 });
                db.Answers.Add(new Answer() { Text = "makes", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary13 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2Vocabulary13 });

                

                // questionA2grammar1
                db.Answers.Add(new Answer() { Text = "since", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar1 });
                db.Answers.Add(new Answer() { Text = "during", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar1 });
                db.Answers.Add(new Answer() { Text = "from ", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar1 });
                db.Answers.Add(new Answer() { Text = "for", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2grammar1 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar1 });

                // questionA2grammar2
                db.Answers.Add(new Answer() { Text = "visits", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar2 });
                db.Answers.Add(new Answer() { Text = "visited", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar2 });
                db.Answers.Add(new Answer() { Text = "is visiting", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar2 });
                db.Answers.Add(new Answer() { Text = "is visited", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2grammar2 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar2 });

                //db.Questions.Add(questionA2grammar3);
                db.Answers.Add(new Answer() { Text = "swimmed", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar3 });
                db.Answers.Add(new Answer() { Text = "swam", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar3 });
                db.Answers.Add(new Answer() { Text = "was swim", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar3 });
                db.Answers.Add(new Answer() { Text = "was swimming", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2grammar3 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar3 });

                //db.Questions.Add(questionA2grammar4);
                db.Answers.Add(new Answer() { Text = "visits", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar4 });
                db.Answers.Add(new Answer() { Text = "visiting", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar4 });
                db.Answers.Add(new Answer() { Text = "will visit", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2grammar4 });
                db.Answers.Add(new Answer() { Text = "visit", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar4 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar4 });

                //db.Questions.Add(questionA2grammar5);
                db.Answers.Add(new Answer() { Text = "more good than", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar5 });
                db.Answers.Add(new Answer() { Text = "best than", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar5 });
                db.Answers.Add(new Answer() { Text = "as better as", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar5 });
                db.Answers.Add(new Answer() { Text = "better than", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2grammar5 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar5 });

                //db.Questions.Add(questionA2grammar6);
                db.Answers.Add(new Answer() { Text = "left", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar6 });
                db.Answers.Add(new Answer() { Text = "had left", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2grammar6 });
                db.Answers.Add(new Answer() { Text = "were left", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar6 });
                db.Answers.Add(new Answer() { Text = "were leaving", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar6 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar6 });

                //db.Questions.Add(questionA2grammar7);
                db.Answers.Add(new Answer() { Text = "mustn't", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar7 });
                db.Answers.Add(new Answer() { Text = "can't", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar7 });
                db.Answers.Add(new Answer() { Text = "don’t have to", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2grammar7 });
                db.Answers.Add(new Answer() { Text = "don’t need", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar7 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar7 });

                //db.Questions.Add(questionA2grammar8);
                db.Answers.Add(new Answer() { Text = "which", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar8 });
                db.Answers.Add(new Answer() { Text = "who", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2grammar8 });
                db.Answers.Add(new Answer() { Text = "what", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar8 });
                db.Answers.Add(new Answer() { Text = "whom", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar8 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar8 });

                //db.Questions.Add(questionA2grammar9);
                db.Answers.Add(new Answer() { Text = "Did you go", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar9 });
                db.Answers.Add(new Answer() { Text = "Have you gone", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar9 });
                db.Answers.Add(new Answer() { Text = "Were you", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar9 });
                db.Answers.Add(new Answer() { Text = "Have you been", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2grammar9 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar9 });

                //db.Questions.Add(questionA2grammar10);
                db.Answers.Add(new Answer() { Text = "I'm seeing", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2grammar10 });
                db.Answers.Add(new Answer() { Text = "I'm going", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar10 });
                db.Answers.Add(new Answer() { Text = "I'll see", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar10 });
                db.Answers.Add(new Answer() { Text = "I see", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar10 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar10 });

                //db.Questions.Add(questionA2grammar11);
                db.Answers.Add(new Answer() { Text = "I didn’t, too.", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar11 });
                db.Answers.Add(new Answer() { Text = "Neither was I.", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2grammar11 });
                db.Answers.Add(new Answer() { Text = "Nor I did.", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar11 });
                db.Answers.Add(new Answer() { Text = "So I wasn’t.", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar11 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar11 });

                //db.Questions.Add(questionA2grammar12);
                db.Answers.Add(new Answer() { Text = "lived", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2grammar12 });
                db.Answers.Add(new Answer() { Text = "has lived", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar12 });
                db.Answers.Add(new Answer() { Text = "has live", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar12 });
                db.Answers.Add(new Answer() { Text = "had live", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar12 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionA2grammar12 });

                // questionA2Reading1
                db.Answers.Add(new Answer() { Text = "True", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA2Reading1 });
                db.Answers.Add(new Answer() { Text = "False", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2Reading1 });
                db.Answers.Add(new Answer() { Text = "No information", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA2Reading1 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA2Reading1 });

                // questionA2Reading2

                db.Answers.Add(new Answer() { Text = "True", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA2Reading2 });
                db.Answers.Add(new Answer() { Text = "False", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA2Reading2 });
                db.Answers.Add(new Answer() { Text = "No information", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2Reading2 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA2Reading2 });

                // questionA2Reading3

                db.Answers.Add(new Answer() { Text = "True", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2Reading3 });
                db.Answers.Add(new Answer() { Text = "False", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA2Reading3 });
                db.Answers.Add(new Answer() { Text = "No information", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA2Reading3 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA2Reading3 });

                // questionA2Reading4

                db.Answers.Add(new Answer() { Text = "True", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2Reading4 });
                db.Answers.Add(new Answer() { Text = "False", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA2Reading4 });
                db.Answers.Add(new Answer() { Text = "No information", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA2Reading4 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA2Reading4 });

                // questionA2Reading5

                db.Answers.Add(new Answer() { Text = "True", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA2Reading5 });
                db.Answers.Add(new Answer() { Text = "False", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2Reading5 });
                db.Answers.Add(new Answer() { Text = "No information", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA2Reading5 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA2Reading5 });

                // questionA2Reading6

                db.Answers.Add(new Answer() { Text = "True", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA2Reading6 });
                db.Answers.Add(new Answer() { Text = "False", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2Reading6 });
                db.Answers.Add(new Answer() { Text = "No information", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA2Reading6 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA2Reading6 });

                //questionA2Reading7

                db.Answers.Add(new Answer() { Text = "concerned", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2Reading7 });
                db.Answers.Add(new Answer() { Text = "improved", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2Reading7 });
                db.Answers.Add(new Answer() { Text = "exploring", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2Reading7 });
                db.Answers.Add(new Answer() { Text = "sights|the sights", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionA2Reading7 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionA2Reading7 });

                #endregion
                
                #region B1

                var questionSetB1Vocabulary1 = new QuestionSet() { Text = @"<p>Vocabulary</p> <p>Complete the sentences with the correct words.</p> <p>Example: I <i>inherited</i> a lot of money a few years ago from my grandfather.</p> <p>Inherited earned invested</p> ", SkillType = SkillType.Vocabulary, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB1 };
                var questionSetB1Reading1 = new QuestionSet() { Text = @"<p>Read the text and tick (ü) A, B, or C.</p> <p>Our facial expression is usually the first indicator of our state of mind. When we’re happy, we smile. And when we’re sad or angry, we frown. There are times, however, when we don’t want people to know what we’re really thinking or feeling, or when we’re trying to hide something. In these situations, we choose our words carefully, and we consciously make our facial expression mirror what we’re saying.</p> <p>However, up to 90 per cent of communication is non-verbal. So we might say one thing, but our body language often tells a different story. Body language refers to the pattern of gestures that express our inner thoughts and feelings in communication.</p> <p>Unless we are very clever, our bodies will usually try to tell the truth, no matter what our words and facial expressions are communicating. Here are three of the most common ways that our bodies can give us away:</p> <p>1) Touching our faces more often than usual. If we are lying, we often cover our mouth with our hand or put a finger on our lip. Part of us knows that what we are saying is not true, and tries to stop it coming out. Touching our ear or hair and, most commonly, our nose are signs that we might be feeling anxious, or that we are angry or frightened but don’t feel able to express it.</p> <p>2) Gesturing with our hands. Experiments have shown that we use our hands to talk with much less than usual when what we are saying is not true. We don’t know exactly what our hands are saying, but we know they are probably communicating something important so we try not to use them. A person who says he or she is very pleased with something, and they have their arms folded while they are speaking, may actually be feeling quite the opposite.</p> <p>3) Moving our legs and feet. These are the most revealing parts of our body as they are the furthest from our face and we don’t usually pay attention to what they are doing. An interviewer might be listening patiently, smiling, and nodding, but if he’s tapping his foot, this could tell us that he is not enjoying the interview at all.</p> <p>Most of us don’t know exactly what someone else’s body language means. But if we feel uneasy in someone’s company, it may be because their words and their body are saying different things from each other. This difference can have a significant effect on how we get on with that person.</p> <p>Example: The expression on our face can _______.</p> <p>A show how we’re feeling c B hide what we really think c <br /> C both show how we’re feeling and hide what we’re really thinking cü</p>", AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB1 };
                var questionSetB1Grammar1 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB1 };
                var questionSetB1Grammar2 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB1 };
                var questionSetB1Grammar3 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB1 };
                var questionSetB1Grammar4 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB1 };
                var questionSetB1Grammar5 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB1 };
                var questionSetB1Grammar6 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB1 };
                var questionSetB1Grammar7 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB1 };
                var questionSetB1Grammar8 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB1 };
                var questionSetB1Grammar9 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB1 };
                var questionSetB1Grammar10 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB1 };
                var questionSetB1Grammar11 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB1 };
                var questionSetB1Grammar12 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB1 };
                var questionSetB1Grammar13 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB1 };
                var questionSetB1Grammar14 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB1 };
                var questionSetB1Grammar15 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB1 };

                db.QuestionSets.Add(questionSetB1Vocabulary1);
                db.QuestionSets.Add(questionSetB1Reading1);
                db.QuestionSets.Add(questionSetB1Grammar1);
                db.QuestionSets.Add(questionSetB1Grammar2);
                db.QuestionSets.Add(questionSetB1Grammar3);
                db.QuestionSets.Add(questionSetB1Grammar4);
                db.QuestionSets.Add(questionSetB1Grammar5);
                db.QuestionSets.Add(questionSetB1Grammar6);
                db.QuestionSets.Add(questionSetB1Grammar7);
                db.QuestionSets.Add(questionSetB1Grammar8);
                db.QuestionSets.Add(questionSetB1Grammar9);
                db.QuestionSets.Add(questionSetB1Grammar10);
                db.QuestionSets.Add(questionSetB1Grammar11);
                db.QuestionSets.Add(questionSetB1Grammar12);
                db.QuestionSets.Add(questionSetB1Grammar13);
                db.QuestionSets.Add(questionSetB1Grammar14);
                db.QuestionSets.Add(questionSetB1Grammar15);
                db.SaveChanges();

                var questionB1Vocabulary1 = new OneAnswerQuestion() { Text = "<p>1 You can’t ride your motorbike through here – it’s a ________ area.</p> <p>[] [] []</p>", QuestionSet = questionSetB1Vocabulary1 };
                var questionB1Vocabulary2 = new OneAnswerQuestion() { Text = "<p>2	I got a 10% ________ on the coat because it had a button missing. </p> <p>[] [] []</p>", QuestionSet = questionSetB1Vocabulary1 };
                var questionB1Vocabulary3 = new OneAnswerQuestion() { Text = "<p>3	Be careful what you say to Maria. She’s very ________.</p> <p>[] [] []</p>", QuestionSet = questionSetB1Vocabulary1 };
                var questionB1Vocabulary4 = new OneAnswerQuestion() { Text = "<p>4 I didn’t have breakfast this morning. I’m absolutely ________!</p><p>[] [] []</p>", QuestionSet = questionSetB1Vocabulary1 };
                var questionB1Vocabulary5 = new OneAnswerQuestion() { Text = "<p>5 Megan was very ________ of her sister after she was promoted.</p><p>[] [] []</p>", QuestionSet = questionSetB1Vocabulary1 };

                var questionB1Reading1 = new OneAnswerQuestion() { Text = "<p>1	We change the expression on our faces when we want people to believe _______.</p> <p>[] [] []</p>", QuestionSet = questionSetB1Reading1 };
                var questionB1Reading2 = new OneAnswerQuestion() { Text = "<p>2	Our body language shows _______.</p> <p>[] [] []</p>", QuestionSet = questionSetB1Reading1 };
                var questionB1Reading3 = new OneAnswerQuestion() { Text = "<p>3	People who aren’t being honest often _______.</p> <p>[] [] []</p>", QuestionSet = questionSetB1Reading1 };
                var questionB1Reading4 = new OneAnswerQuestion() { Text = "<p>4	People who are afraid tend to _______.</p> <p>[] [] []</p>", QuestionSet = questionSetB1Reading1 };
                var questionB1Reading5 = new OneAnswerQuestion() { Text = "<p>5	It’s common to _______ if we aren’t telling the truth.</p> <p>[] [] []</p>", QuestionSet = questionSetB1Reading1 };
                var questionB1Reading6 = new OneAnswerQuestion() { Text = "<p>6	To decide if someone is telling the truth, looking at their hands is _______ listening to what they say.</p> <p>[] [] []</p>", QuestionSet = questionSetB1Reading1 };
                var questionB1Reading7 = new OneAnswerQuestion() { Text = "<p>7	When it comes to watching body language, legs and feet _______.</p> <p>[] [] []</p>", QuestionSet = questionSetB1Reading1 };
                var questionB1Reading8 = new OneAnswerQuestion() { Text = "<p>8	If an interviewer’s foot is moving, he’s probably _______.</p> <p>[] [] []</p>", QuestionSet = questionSetB1Reading1 };
                var questionB1Reading9 = new OneAnswerQuestion() { Text = "<p>9	_______ can interpret a person’s body language accurately.</p> <p>[] [] []</p>", QuestionSet = questionSetB1Reading1 };
                var questionB1Reading10 = new OneAnswerQuestion() { Text = "<p>10 If a person’s words and body language don’t match, we can feel _______.</p> <p>[] [] []</p>", QuestionSet = questionSetB1Reading1 };
                var questionB1Reading11 = new InputAnswerQuestion() { Text = "<p>2 Match five of the highlighted words and phrases with the definitions.</p> <p>1 crossed in front of your chest</p> <p>[]</p> <p>2 moving the head up and down</p> <p>[]</p> <p>3 regular way things happen</p> <p>[]</p> <p>4 letting something be known that is usually hidden</p> <p>[]</p> <p>5 make lines appear in the space above your eyes</p> <p> [] </p> ", QuestionSet = questionSetB1Reading1 };

                var questionB1grammar1 = new OneAnswerQuestion() { Text = "My only sister, ________________ is married, lives in Bristol. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetB1Grammar1 };
                var questionB1grammar2 = new OneAnswerQuestion() { Text = "The policeman asked him ___________________ . <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetB1Grammar2 };
                var questionB1grammar3 = new OneAnswerQuestion() { Text = "My son got injured while he___________________ basketball last Saturday. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetB1Grammar3 };
                var questionB1grammar4 = new OneAnswerQuestion() { Text = "I can't go out now. I___________________ . <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetB1Grammar4 };
                var questionB1grammar5 = new OneAnswerQuestion() { Text = "Kate ___________________ to have dinner right now. She's not at all hungry. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetB1Grammar5 };
                var questionB1grammar6 = new OneAnswerQuestion() { Text = "We ___________________ at 10:00 o'clock tomorrow in Jack's office. We arranged it weeks ago. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetB1Grammar6 };
                var questionB1grammar7 = new OneAnswerQuestion() { Text = "I___________________  to last night's party if I'd known Lisa was there. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetB1Grammar7 };
                var questionB1grammar8 = new OneAnswerQuestion() { Text = "When we finally arrived at the airport, we were told that our plane ___________________ 5 minutes earlier. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetB1Grammar8 };
                var questionB1grammar9 = new OneAnswerQuestion() { Text = "Unfortunately,  I'm ___________________  I used to be when I first started working in this job. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetB1Grammar9 };
                var questionB1grammar10 = new OneAnswerQuestion() { Text = "Could you possibly give me a lift? My car ___________________ today. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetB1Grammar10 };
                var questionB1grammar11 = new OneAnswerQuestion() { Text = "__________________ this great book for a week now and I can’t wait to see how it ends. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetB1Grammar11 };
                var questionB1grammar12 = new OneAnswerQuestion() { Text = "You _________________ him in London yesterday. He left for Italy a week ago. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetB1Grammar12 };
                var questionB1grammar13 = new OneAnswerQuestion() { Text = "That girl took your handbag,  __________________ ? <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetB1Grammar13 };
                var questionB1grammar14 = new OneAnswerQuestion() { Text = "Someone suggested __________________ a different route to avoid the roadworks. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetB1Grammar14 };
                var questionB1grammar15 = new OneAnswerQuestion() { Text = "You  ____________ park here. If you do, you'll get a fine. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetB1Grammar15 };

                db.Questions.Add(questionB1Vocabulary1);
                db.Questions.Add(questionB1Vocabulary2);
                db.Questions.Add(questionB1Vocabulary3);
                db.Questions.Add(questionB1Vocabulary4);
                db.Questions.Add(questionB1Vocabulary5);

                db.Questions.Add(questionB1Reading1);
                db.Questions.Add(questionB1Reading2);
                db.Questions.Add(questionB1Reading3);
                db.Questions.Add(questionB1Reading4);
                db.Questions.Add(questionB1Reading5);
                db.Questions.Add(questionB1Reading6);
                db.Questions.Add(questionB1Reading7);
                db.Questions.Add(questionB1Reading8);
                db.Questions.Add(questionB1Reading9);
                db.Questions.Add(questionB1Reading10);
                db.Questions.Add(questionB1Reading11);

                db.Questions.Add(questionB1grammar1);
                db.Questions.Add(questionB1grammar2);
                db.Questions.Add(questionB1grammar3);
                db.Questions.Add(questionB1grammar4);
                db.Questions.Add(questionB1grammar5);
                db.Questions.Add(questionB1grammar6);
                db.Questions.Add(questionB1grammar7);
                db.Questions.Add(questionB1grammar8);
                db.Questions.Add(questionB1grammar9);
                db.Questions.Add(questionB1grammar10);
                db.Questions.Add(questionB1grammar11);
                db.Questions.Add(questionB1grammar12);
                db.Questions.Add(questionB1grammar13);
                db.Questions.Add(questionB1grammar14);
                db.Questions.Add(questionB1grammar15);

                // questionB1Vocabulary1
                db.Answers.Add(new Answer() { Text = "residential", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Vocabulary1 });
                db.Answers.Add(new Answer() { Text = "pedestrian", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1Vocabulary1 });
                db.Answers.Add(new Answer() { Text = "suburb", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Vocabulary1 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Vocabulary1 });

                // questionB1Vocabulary2
                db.Answers.Add(new Answer() { Text = "refund", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Vocabulary2 });
                db.Answers.Add(new Answer() { Text = "bargain", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Vocabulary2 });
                db.Answers.Add(new Answer() { Text = "discount", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1Vocabulary2 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Vocabulary2 });

                // questionB1Vocabulary3
                db.Answers.Add(new Answer() { Text = "reliable", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Vocabulary3 });
                db.Answers.Add(new Answer() { Text = "sensible", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Vocabulary3 });
                db.Answers.Add(new Answer() { Text = "sensitive", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1Vocabulary3 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Vocabulary3 });

                // questionB1Vocabulary4
                db.Answers.Add(new Answer() { Text = "starving", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1Vocabulary4 });
                db.Answers.Add(new Answer() { Text = "furious", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Vocabulary4 });
                db.Answers.Add(new Answer() { Text = "freezing", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Vocabulary4 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Vocabulary4 });

                // questionB1Vocabulary5
                db.Answers.Add(new Answer() { Text = "jealous", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1Vocabulary5 });
                db.Answers.Add(new Answer() { Text = "ambitious", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Vocabulary5 });
                db.Answers.Add(new Answer() { Text = "moody", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Vocabulary5 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Vocabulary5 });


                // questionB1Reading1
                db.Answers.Add(new Answer() { Text = "what we're really thinking", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading1 });
                db.Answers.Add(new Answer() { Text = "that we're lying", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading1 });
                db.Answers.Add(new Answer() { Text = "what we're saying", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1Reading1 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading1 });

                // questionB1Reading2
                db.Answers.Add(new Answer() { Text = "what we want people to think", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading2 });
                db.Answers.Add(new Answer() { Text = "what we're really thinking", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1Reading2 });
                db.Answers.Add(new Answer() { Text = "that we always tell the truth", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading2 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading2 });

                // questionB1Reading3
                db.Answers.Add(new Answer() { Text = "touch their faces more frequently", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1Reading3 });
                db.Answers.Add(new Answer() { Text = "try to stop talking", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading3 });
                db.Answers.Add(new Answer() { Text = "touch their hair", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading3 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading3 });

                // questionB1Reading4
                db.Answers.Add(new Answer() { Text = "cover their mouths", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading4 });
                db.Answers.Add(new Answer() { Text = "touch their noses", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1Reading4 });
                db.Answers.Add(new Answer() { Text = "touch their hands", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading4 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading4 });

                // questionB1Reading5
                db.Answers.Add(new Answer() { Text = "use our hands more", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading5 });
                db.Answers.Add(new Answer() { Text = "use our hands less", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1Reading5 });
                db.Answers.Add(new Answer() { Text = "look at our hands", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading5 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading5 });

                // questionB1Reading6
                db.Answers.Add(new Answer() { Text = "a better indication than", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1Reading6 });
                db.Answers.Add(new Answer() { Text = "just as effective as", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading6 });
                db.Answers.Add(new Answer() { Text = "not as effective as", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading6 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading6 });

                // questionB1Reading7
                db.Answers.Add(new Answer() { Text = "aren't as interesting as faces", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading7 });
                db.Answers.Add(new Answer() { Text = "are the most revealing", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1Reading7 });
                db.Answers.Add(new Answer() { Text = "aren't worth looking at", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading7 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading7 });

                // questionB1Reading8
                db.Answers.Add(new Answer() { Text = "listening very carefully", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading8 });
                db.Answers.Add(new Answer() { Text = "not enjoying the interview", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1Reading8 });
                db.Answers.Add(new Answer() { Text = "not paying attention", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading8 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading8 });

                // questionB1Reading9
                db.Answers.Add(new Answer() { Text = "Few people", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1Reading9 });
                db.Answers.Add(new Answer() { Text = "Nobody", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading9 });
                db.Answers.Add(new Answer() { Text = "Most people", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading9 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading9 });

                // questionB1Reading10
                db.Answers.Add(new Answer() { Text = "at ease", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading10 });
                db.Answers.Add(new Answer() { Text = "relaxed", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading10 });
                db.Answers.Add(new Answer() { Text = "uncomfortable", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1Reading10 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading10 });

                //questionB1Reading11
                db.Answers.Add(new Answer() { Text = "folded", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1Reading11 });
                db.Answers.Add(new Answer() { Text = "nodding", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1Reading11 });
                db.Answers.Add(new Answer() { Text = "pattern", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1Reading11 });
                db.Answers.Add(new Answer() { Text = "revealing", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1Reading11 });
                db.Answers.Add(new Answer() { Text = "frown", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1Reading11 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 4, NegativePoints = 0, Question = questionB1Reading11 });

                // questionB1grammar1
                db.Answers.Add(new Answer() { Text = "which", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar1 });
                db.Answers.Add(new Answer() { Text = "who", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1grammar1 });
                db.Answers.Add(new Answer() { Text = "that", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar1 });
                db.Answers.Add(new Answer() { Text = "whom", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar1 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar1 });

                // questionB1grammar2
                db.Answers.Add(new Answer() { Text = "what was his name", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar2 });
                db.Answers.Add(new Answer() { Text = "what is his name", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar2 });
                db.Answers.Add(new Answer() { Text = "if his name was", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar2 });
                db.Answers.Add(new Answer() { Text = "what his name was", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1grammar2 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar2 });

                //db.Questions.Add(questionB1grammar3);
                db.Answers.Add(new Answer() { Text = "playing", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar3 });
                db.Answers.Add(new Answer() { Text = "was playing", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1grammar3 });
                db.Answers.Add(new Answer() { Text = "played", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar3 });
                db.Answers.Add(new Answer() { Text = "had played", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar3 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar3 });

                //db.Questions.Add(questionB1grammar4);
                db.Answers.Add(new Answer() { Text = "haven't finish my homework yet", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar4 });
                db.Answers.Add(new Answer() { Text = "haven't finished my homework already", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar4 });
                db.Answers.Add(new Answer() { Text = "didn't finish my homework yet", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar4 });
                db.Answers.Add(new Answer() { Text = "haven't finished my homework yet", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1grammar4 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar4 });

                //db.Questions.Add(questionB1grammar5);
                db.Answers.Add(new Answer() { Text = "Don't want", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar5 });
                db.Answers.Add(new Answer() { Text = "doesn't want", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1grammar5 });
                db.Answers.Add(new Answer() { Text = "isn't wanting", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar5 });
                db.Answers.Add(new Answer() { Text = "wasn't wanting", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar5 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar5 });

                //db.Questions.Add(questionB1grammar6);
                db.Answers.Add(new Answer() { Text = "will meet", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar6 });
                db.Answers.Add(new Answer() { Text = "meet", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar6 });
                db.Answers.Add(new Answer() { Text = "are meeting", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1grammar6 });
                db.Answers.Add(new Answer() { Text = "are going to meeting", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar6 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar6 });

                //db.Questions.Add(questionB1grammar7);
                db.Answers.Add(new Answer() { Text = "wouldn't go", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar7 });
                db.Answers.Add(new Answer() { Text = "hadn't gone", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar7 });
                db.Answers.Add(new Answer() { Text = "haven't gone", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar7 });
                db.Answers.Add(new Answer() { Text = "wouldn't have gone", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1grammar7 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar7 });

                //db.Questions.Add(questionB1grammar8);
                db.Answers.Add(new Answer() { Text = "had taken off", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1grammar8 });
                db.Answers.Add(new Answer() { Text = "was taking off", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar8 });
                db.Answers.Add(new Answer() { Text = "took off", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar8 });
                db.Answers.Add(new Answer() { Text = "has taken off", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar8 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar8 });

                //db.Questions.Add(questionB1grammar9);
                db.Answers.Add(new Answer() { Text = "less excited that", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar9 });
                db.Answers.Add(new Answer() { Text = "least excited than", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar9 });
                db.Answers.Add(new Answer() { Text = "not so excited that", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar9 });
                db.Answers.Add(new Answer() { Text = "not as excited as", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1grammar9 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar9 });

                //db.Questions.Add(questionB1grammar10);
                db.Answers.Add(new Answer() { Text = "Is repairing", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar10 });
                db.Answers.Add(new Answer() { Text = "is repaired", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar10 });
                db.Answers.Add(new Answer() { Text = "is been repaired", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar10 });
                db.Answers.Add(new Answer() { Text = "is being repaired", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1grammar10 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar10 });

                //db.Questions.Add(questionB1grammar11);
                db.Answers.Add(new Answer() { Text = "I'm reading", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar11 });
                db.Answers.Add(new Answer() { Text = "I’ve read", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar11 });
                db.Answers.Add(new Answer() { Text = "I’ve been reading", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1grammar11 });
                db.Answers.Add(new Answer() { Text = "I read", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar11 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar11 });

                //db.Questions.Add(questionB1grammar12);
                db.Answers.Add(new Answer() { Text = "mustn't see", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar12 });
                db.Answers.Add(new Answer() { Text = "can’t see", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar12 });
                db.Answers.Add(new Answer() { Text = "mustn’t have seen", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar12 });
                db.Answers.Add(new Answer() { Text = "can’t have seen", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1grammar12 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar12 });

                //db.Questions.Add(questionB1grammar13);
                db.Answers.Add(new Answer() { Text = "did she", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar13 });
                db.Answers.Add(new Answer() { Text = "didn't she", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1grammar13 });
                db.Answers.Add(new Answer() { Text = "does she", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar13 });
                db.Answers.Add(new Answer() { Text = "wasn't she", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar13 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar13 });

                //db.Questions.Add(questionB1grammar14);
                db.Answers.Add(new Answer() { Text = "us to take", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar14 });
                db.Answers.Add(new Answer() { Text = "to take", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar14 });
                db.Answers.Add(new Answer() { Text = "taking", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1grammar14 });
                db.Answers.Add(new Answer() { Text = "us taking", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar14 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar14 });

                //db.Questions.Add(questionB1grammar15);
                db.Answers.Add(new Answer() { Text = "Don't have to", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar15 });
                db.Answers.Add(new Answer() { Text = "shouldn't", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar15 });
                db.Answers.Add(new Answer() { Text = "mustn't", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB1grammar15 });
                db.Answers.Add(new Answer() { Text = "ought not to", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar15 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB1grammar15 });

                #endregion // unfinished

                #region B2

                var questionSetB2Vocabulary1 = new QuestionSet() { Text = "<p>VOCABULARY</p>", SkillType = SkillType.Vocabulary, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB2 };
                var questionSetB2Reading1 = new QuestionSet() { Text = "<p>READING</p>", SkillType = SkillType.Reading, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB2 };



                

                var questionB2Vocabulary1 = new OneAnswerQuestion() { Text = "1 The drug was withdrawn because of its harmful side [] [].", QuestionSet = questionSetB2Vocabulary1 };
                var questionB2Vocabulary2 = new OneAnswerQuestion() { Text = "2[] []me to buy some stamps when we’re in the newsagents.", QuestionSet = questionSetB2Vocabulary1 };
                var questionB2Vocabulary3 = new OneAnswerQuestion() { Text = "3 My wallet was [] [] from out of my jacket when I went to the toilet.", QuestionSet = questionSetB2Vocabulary1 };
                var questionB2Vocabulary4 = new OneAnswerQuestion() { Text = "4 It was a long and difficult journey, but we arrived [] [].", QuestionSet = questionSetB2Vocabulary1 };
                var questionB2Vocabulary5 = new OneAnswerQuestion() { Text = "5 This branch [] [] the biggest profit last year.", QuestionSet = questionSetB2Vocabulary1 };
                var questionB2Vocabulary6 = new OneAnswerQuestion() { Text = "6 It’s hard to find [] [] journalism that isn’t obviously left- or right-wing.", QuestionSet = questionSetB2Vocabulary1 };
                var questionB2Vocabulary7 = new OneAnswerQuestion() { Text = "7 I’ve had plenty of downs and [] [] in my 20-year career.", QuestionSet = questionSetB2Vocabulary1 };
                var questionB2Vocabulary8 = new OneAnswerQuestion() { Text = "8 Prices have [] [] by over 10% during the last year.", QuestionSet = questionSetB2Vocabulary1 };

                var questionB2reading1 = new InputAnswerQuestion() { Text = "<p><b>Read the text about tattoos. 6 sentences have been taken out of the text and put in a jumbled order after it. Decide where in the text each sentence was taken from. There is one extra sentence that you don't need.</b></p> <p>Marked for Life</p> <p>We were having a cup of coffee at a street cafe when my friend Mark, getting too hot in the July sun, rolled up his sleeves. I then found something out about him I didn’t know before – he had a tattoo!</p> <p>1. []</p> <p>So when did this start? ‘It must be about ten years ago now,’ Mark said. ‘I was</p> <p>travelling around Asia and tattoos were becoming very fashionable. I thought the designs looked really nice and I chose a dolphin – it seemed appropriate as I was on an island in the middle of an ocean.’</p> <p>Yes, it’s certainly quite fashionable now – especially among the young. Yet, when I was young tattoos were associated with sailors, bikers and men working at the circus. Why has this changed? ‘I suppose because it’s seen as doing something different, a little risqué or radical and that appeals to young people.</p> <p>2. []</p> <p>There, even a very small tattoo can cost $50. And did you know that it wasn’t until 1999 that New York City lifted its ban on tattooing!’ Mark explained. But as my friend reminded me this is by no means the first time that tattoos have been fashionable.</p> <p>3. []</p> <p>It’s been suggested that tattooing may have started in Egypt because tattoos were found on Egyptian mummies from about 2000 BC. ‘Ancient Maori warriors,’ Mark continued ‘used to paint their faces with charcoal before a battle. Afterwards, they then began to make the lines permanent rather than reapplying the charcoal for each battle. In Borneo men were covered in images of plants and creatures. This provided both camouflage and protection against evil spirits. And in the South Seas Islands, particularly Samoa, tattooing continues to be an art form since it was first noted in the 19th century.’ It seems that the first documented evidence of tattooing in Britain was in 787 AD when it was outlawed by the British Council of Churches as a pagan practice. However, this didn’t stop people, including royalty. King Harold’s body was only identified after the battle of Hastings in 1066 by his tattoos. In the late 19th century, tattoos were popular among wealthy socialites. Lady Randolph Churchill, mother of Winston Churchill, had a small tattoo of a coiled snake around her wrist. Even Queen Victoria herself was rumoured to have a small, discreet tattoo.</p> <p>4. []</p> <p>And what came after the dolphin? ‘Well, I continued the marine theme with different kinds of fish, then an octopus and a starfish. These are all on my left leg. Later, I decided to have a school of barracuda swimming over my left shoulder and the top of my arm. I also have a huge shark on my back, chest and other arm. ’</p> <p>Does it hurt to have a tattoo? ‘It depends! If you’re feeling relaxed, then usually not. But if you’re tired or nervous, then you’re more sensitive to pain. I met somebody who told me that they fall asleep during the tattooing – so it certainly didn’t bother them! Another factor is where you are getting it.</p> <p>5. []</p> <p>And it can be very swollen afterwards, too.’</p> <p>Is it safe? ‘If it’s done in a licensed place, then yes. This means that precautions are taken at every stage, such as fresh needles for each person, everything sterilized and unused ink being thrown away and not returned to the bottle. However,’ Mark added, ‘before the advent of antibiotics in the 1940’s there was an alarming rate of infection sometimes leading to the death of the tattooed – fortunately, that’s no longer a problem.’</p> <p>And if you had any advice for someone who’s thinking about getting a tattoo, what would that be? ‘Don’t be too serious about it!</p> <p>6. []</p> <p>Then ten years later their lives have changed and those influences are less meaningful. As a result, they’re stuck with something they don’t want. So have one, but do it for fun!’</p> <p>Finally, where did the word tattoo come from? ‘It’s from the Tahitian word tattau which means – wait for it – ‘to mark’!’ he laughed.</p> <p>A. The people who regret having had a tattoo are those who thought for ages about what would be most appropriate and then chose the name of their girlfriend or boyfriend, or the name of their favourite pop group.</p> <p>B. Although it’s not certain exactly where tattooing started there is lots of evidence that for millions of people throughout time it has been one of the most popular forms of permanent body art.</p> <p>C. ‘Why yes,’ he said to my surprised reaction, ‘in fact I have many tattoos – on my arms, legs, chest and back!’</p> <p>D. Choose a good tattoo artist, a design you really like looking at and some area of the body that will be easier to cover, should the situation call for it.</p> <p>E. It wasn’t until later, as costs fell, that tattoos became more commonly associated with sailors and criminals.</p> <p>F. Most people start with the top of the arm as this is usually fine, but if the tattoo is over a bone, for example on your foot, then that’s a different matter.</p> <p>G. And in Asia it’s both easier and cheaper to get a tattoo than, say, the US.</p>", QuestionSet = questionSetB2Reading1 };
                var questionB2reading2 = new InputAnswerQuestion() { Text = "<p>Read the text about tattoos again. Write the appropriate highlighted expressions across from their correct definitions, keeping in mind their usage in the text. There are extra expressions that you don't need.</p> <p>1. []- a large number of sea creatures swimming in a group</p> <p><br /> </p> <p>2. []- unable to change or get away from a situation</p> <p><br /> </p> <p>3. []- actions taken to prevent something unpleasant or dangerous from happening</p> <p><br /> </p> <p>4. []- someone, usually of high social class, who is famous for going to a lot of parties and social events</p> <br />", QuestionSet = questionSetB2Reading1 };

                var questionSetB2grammar1 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB2 };
                var questionB2grammar1 = new OneAnswerQuestion() { Text = "We would like to have ______ information about your services.<p>[]</p> <p>[]</p> <p>[]</p><p>[]</p>", QuestionSet = questionSetB2grammar1 };
                var questionSetB2grammar2 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB2 };
                var questionB2grammar2 = new OneAnswerQuestion() { Text = "The burglars ______ by the time the police arrived at the scene.<p>[]</p> <p>[]</p> <p>[]</p><p>[]</p>", QuestionSet = questionSetB2grammar2 };
                var questionSetB2grammar3 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB2 };
                var questionB2grammar3 = new OneAnswerQuestion() { Text = "Person A: 'I go clubbing most weekends. Person B: ______ you? I can't afford to go out that often.<p>[]</p> <p>[]</p> <p>[]</p><p>[]</p>'", QuestionSet = questionSetB2grammar3 };
                var questionSetB2grammar4 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB2 };
                var questionB2grammar4 = new OneAnswerQuestion() { Text = "Paul's starting to get impatient. He ______ for about an hour now.<p>[]</p> <p>[]</p> <p>[]</p><p>[]</p>", QuestionSet = questionSetB2grammar4 };
                var questionSetB2grammar5 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB2 };
                var questionB2grammar5 = new OneAnswerQuestion() { Text = "This is the first time Annie ______ abroad.<p>[]</p> <p>[]</p> <p>[]</p><p>[]</p> ", QuestionSet = questionSetB2grammar5 };
                var questionSetB2grammar6 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB2 };
                var questionB2grammar6 = new OneAnswerQuestion() { Text = "If I ______ , I wouldn't have been an hour late for work this morning.<p>[]</p> <p>[]</p> <p>[]</p><p>[]</p> ", QuestionSet = questionSetB2grammar6 };
                var questionSetB2grammar7 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB2 };
                var questionB2grammar7 = new OneAnswerQuestion() { Text = "We could safely say that the criminals  ______ through that door. We found it still locked.<p>[]</p> <p>[]</p> <p>[]</p><p>[]</p>", QuestionSet = questionSetB2grammar7 };
                var questionSetB2grammar8 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB2 };
                var questionB2grammar8 = new OneAnswerQuestion() { Text = "I'd rather ______  in than go out tonight. <p>[]</p> <p>[]</p> <p>[]</p><p>[]</p>", QuestionSet = questionSetB2grammar8 };
                var questionSetB2grammar9 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB2 };
                var questionB2grammar9 = new OneAnswerQuestion() { Text = "He complained that nobody ______ to dance with him during the previous night's party.<p>[]</p> <p>[]</p> <p>[]</p><p>[]</p>", QuestionSet = questionSetB2grammar9 };
                var questionSetB2grammar10 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB2 };
                var questionB2grammar10 = new OneAnswerQuestion() { Text = "When trying to pay the bill, Sue realised she'd forgotten ______ any money with her.<p>[]</p> <p>[]</p> <p>[]</p><p>[]</p>", QuestionSet = questionSetB2grammar10 };
                var questionSetB2grammar11 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB2 };
                var questionB2grammar11 = new OneAnswerQuestion() { Text = "By the end of the week, we ______ into our new flat.<p>[]</p> <p>[]</p> <p>[]</p><p>[]</p>", QuestionSet = questionSetB2grammar11 };
                var questionSetB2grammar12 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB2 };
                var questionB2grammar12 = new OneAnswerQuestion() { Text = "The injured hiker ______ there for hours before he was spotted by mountain rescuers.<p>[]</p> <p>[]</p> <p>[]</p><p>[]</p>", QuestionSet = questionSetB2grammar12 };
                var questionSetB2grammar13 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB2 };
                var questionB2grammar13 = new OneAnswerQuestion() { Text = "I wish I ______ about this before the meeting last week.<p>[]</p> <p>[]</p> <p>[]</p><p>[]</p>", QuestionSet = questionSetB2grammar13 };
                var questionSetB2grammar14 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB2 };
                var questionB2grammar14 = new OneAnswerQuestion() { Text = "Do you have any idea how much ______ .<p>[]</p> <p>[]</p> <p>[]</p><p>[]</p>", QuestionSet = questionSetB2grammar14 };
                var questionSetB2grammar15 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB2 };
                var questionB2grammar15 = new OneAnswerQuestion() { Text = "At the time of the elections, the politician ______ on corruption charges.<p>[]</p> <p>[]</p> <p>[]</p><p>[]</p>", QuestionSet = questionSetB2grammar15 };
                var questionSetB2grammar16 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB2 };
                var questionB2grammar16 = new OneAnswerQuestion() { Text = "If I had made different life choices, I probably ______ where I am today.<p>[]</p> <p>[]</p> <p>[]</p><p>[]</p>", QuestionSet = questionSetB2grammar16 };
                var questionSetB2grammar17 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB2 };
                var questionB2grammar17 = new OneAnswerQuestion() { Text = "I found winters there unbearable as I ______ in such a cold climate.<p>[]</p> <p>[]</p> <p>[]</p><p>[]</p>", QuestionSet = questionSetB2grammar17 };

                db.QuestionSets.Add(questionSetB2Vocabulary1);
                db.QuestionSets.Add(questionSetB2Reading1);
                db.QuestionSets.Add(questionSetB2grammar1);
                db.QuestionSets.Add(questionSetB2grammar2);
                db.QuestionSets.Add(questionSetB2grammar3);
                db.QuestionSets.Add(questionSetB2grammar4);
                db.QuestionSets.Add(questionSetB2grammar5);
                db.QuestionSets.Add(questionSetB2grammar6);
                db.QuestionSets.Add(questionSetB2grammar7);
                db.QuestionSets.Add(questionSetB2grammar8);
                db.QuestionSets.Add(questionSetB2grammar9);
                db.QuestionSets.Add(questionSetB2grammar10);
                db.QuestionSets.Add(questionSetB2grammar11);
                db.QuestionSets.Add(questionSetB2grammar12);
                db.QuestionSets.Add(questionSetB2grammar13);
                db.QuestionSets.Add(questionSetB2grammar14);
                db.QuestionSets.Add(questionSetB2grammar15);
                db.QuestionSets.Add(questionSetB2grammar16);
                db.QuestionSets.Add(questionSetB2grammar17);


                db.Questions.Add(questionB2Vocabulary1);
                db.Questions.Add(questionB2Vocabulary2);
                db.Questions.Add(questionB2Vocabulary3);
                db.Questions.Add(questionB2Vocabulary4);
                db.Questions.Add(questionB2Vocabulary5);
                db.Questions.Add(questionB2Vocabulary6);
                db.Questions.Add(questionB2Vocabulary7);
                db.Questions.Add(questionB2Vocabulary8);
                db.Questions.Add(questionB2reading1);
                db.Questions.Add(questionB2reading2);
                db.Questions.Add(questionB2grammar1);
                db.Questions.Add(questionB2grammar2);
                db.Questions.Add(questionB2grammar3);
                db.Questions.Add(questionB2grammar4);
                db.Questions.Add(questionB2grammar5);
                db.Questions.Add(questionB2grammar6);
                db.Questions.Add(questionB2grammar7);
                db.Questions.Add(questionB2grammar8);
                db.Questions.Add(questionB2grammar9);
                db.Questions.Add(questionB2grammar10);
                db.Questions.Add(questionB2grammar11);
                db.Questions.Add(questionB2grammar12);
                db.Questions.Add(questionB2grammar13);
                db.Questions.Add(questionB2grammar14);
                db.Questions.Add(questionB2grammar15);
                db.Questions.Add(questionB2grammar16);
                db.Questions.Add(questionB2grammar17);
                
                db.SaveChanges();
             



                db.Answers.Add(new Answer() { Text = "results", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2Vocabulary1 });
                db.Answers.Add(new Answer() { Text = "effects", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2Vocabulary1 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2Vocabulary1 });

                db.Answers.Add(new Answer() { Text = "Remember", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2Vocabulary2 });
                db.Answers.Add(new Answer() { Text = "Remind", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2Vocabulary2 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2Vocabulary2 });

                db.Answers.Add(new Answer() { Text = "stolen", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2Vocabulary3 });
                db.Answers.Add(new Answer() { Text = "robbed", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2Vocabulary3 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2Vocabulary3 });

                db.Answers.Add(new Answer() { Text = "safe and sound ", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2Vocabulary4});
                db.Answers.Add(new Answer() { Text = "sound and safe", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2Vocabulary4 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2Vocabulary4 });

                db.Answers.Add(new Answer() { Text = "did", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2Vocabulary5 });
                db.Answers.Add(new Answer() { Text = "made ", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2Vocabulary5 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2Vocabulary5 });

                db.Answers.Add(new Answer() { Text = "biased ", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2Vocabulary6 });
                db.Answers.Add(new Answer() { Text = "objective ", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2Vocabulary6 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2Vocabulary6 });

                db.Answers.Add(new Answer() { Text = "downs and outs ", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2Vocabulary7 });
                db.Answers.Add(new Answer() { Text = "ups and downs ", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2Vocabulary7 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2Vocabulary7 });

                db.Answers.Add(new Answer() { Text = "raised", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2Vocabulary8 });
                db.Answers.Add(new Answer() { Text = "risen", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2Vocabulary8 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2Vocabulary8 });

                db.Answers.Add(new Answer() { Text = "C", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2reading1 });
                db.Answers.Add(new Answer() { Text = "G", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2reading1 });
                db.Answers.Add(new Answer() { Text = "B", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2reading1 });
                db.Answers.Add(new Answer() { Text = "E", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2reading1 });
                db.Answers.Add(new Answer() { Text = "F", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2reading1 });
                db.Answers.Add(new Answer() { Text = "A", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2reading1 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2reading1 });

                db.Answers.Add(new Answer() { Text = "school", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2reading2 });
                db.Answers.Add(new Answer() { Text = "stuck", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2reading2 });
                db.Answers.Add(new Answer() { Text = "precautions", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2reading2 });
                db.Answers.Add(new Answer() { Text = "socialites|socialite", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2reading2 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2reading2 });

                // questionB2grammar1
                db.Answers.Add(new Answer() { Text = "Some", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2grammar1 });
                db.Answers.Add(new Answer() { Text = "any", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar1 });
                db.Answers.Add(new Answer() { Text = "an", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar1 });
                db.Answers.Add(new Answer() { Text = "a", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar1 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar1 });

                // questionB2grammar2
                db.Answers.Add(new Answer() { Text = "left", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar2 });
                db.Answers.Add(new Answer() { Text = "had left", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2grammar2 });
                db.Answers.Add(new Answer() { Text = "were left", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar2 });
                db.Answers.Add(new Answer() { Text = "were leaving", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar2 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar2 });

                //db.Questions.Add(questionB2grammar3);
                db.Answers.Add(new Answer() { Text = "do", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2grammar3 });
                db.Answers.Add(new Answer() { Text = "don't", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar3 });
                db.Answers.Add(new Answer() { Text = "aren't", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar3 });
                db.Answers.Add(new Answer() { Text = "are", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar3 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar3 });

                //db.Questions.Add(questionB2grammar4);
                db.Answers.Add(new Answer() { Text = "has been waiting", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2grammar4 });
                db.Answers.Add(new Answer() { Text = "has waited", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar4 });
                db.Answers.Add(new Answer() { Text = "is waiting", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar4 });
                db.Answers.Add(new Answer() { Text = "was waiting", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar4 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar4 });

                //db.Questions.Add(questionB2grammar5);
                db.Answers.Add(new Answer() { Text = "has been travelling", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar5 });
                db.Answers.Add(new Answer() { Text = "has travelled", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2grammar5 });
                db.Answers.Add(new Answer() { Text = "travels", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar5 });
                db.Answers.Add(new Answer() { Text = "is travelling", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar5 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar5 });

                //db.Questions.Add(questionB2grammar6);
                db.Answers.Add(new Answer() { Text = "Had overslept", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar6 });
                db.Answers.Add(new Answer() { Text = "didn't oversleep", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar6 });
                db.Answers.Add(new Answer() { Text = "wasn't overslept", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar6 });
                db.Answers.Add(new Answer() { Text = "hadn't overslept", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2grammar6 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar6 });

                //db.Questions.Add(questionB2grammar7);
                db.Answers.Add(new Answer() { Text = "Mustn't have escaped", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar7 });
                db.Answers.Add(new Answer() { Text = "can't have escaped", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2grammar7 });
                db.Answers.Add(new Answer() { Text = "may not have escaped", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar7 });
                db.Answers.Add(new Answer() { Text = "didn't have escaped", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar7 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar7 });

                //db.Questions.Add(questionB2grammar8);
                db.Answers.Add(new Answer() { Text = "to eat", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar8 });
                db.Answers.Add(new Answer() { Text = "eating", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar8 });
                db.Answers.Add(new Answer() { Text = "eat", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2grammar8 });
                db.Answers.Add(new Answer() { Text = "ate", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar8 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar8 });

                //db.Questions.Add(questionB2grammar9);
                db.Answers.Add(new Answer() { Text = "wants", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar9 });
                db.Answers.Add(new Answer() { Text = "wanted", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar9 });
                db.Answers.Add(new Answer() { Text = "didn’t want", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar9 });
                db.Answers.Add(new Answer() { Text = "had wanted", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2grammar9 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar9 });

                //db.Questions.Add(questionB2grammar10);
                db.Answers.Add(new Answer() { Text = "to take", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2grammar10 });
                db.Answers.Add(new Answer() { Text = "taking", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar10 });
                db.Answers.Add(new Answer() { Text = "having taken", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar10 });
                db.Answers.Add(new Answer() { Text = "to have taken", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar10 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar10 });

                //db.Questions.Add(questionB2grammar11);
                db.Answers.Add(new Answer() { Text = "will be moving", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar11 });
                db.Answers.Add(new Answer() { Text = "will move", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar11 });
                db.Answers.Add(new Answer() { Text = "will moved", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar11 });
                db.Answers.Add(new Answer() { Text = "will have moved", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2grammar11 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar11 });

                //db.Questions.Add(questionB2grammar12);
                db.Answers.Add(new Answer() { Text = "had lied", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar12 });
                db.Answers.Add(new Answer() { Text = "had lain", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar12 });
                db.Answers.Add(new Answer() { Text = "had been lying", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2grammar12 });
                db.Answers.Add(new Answer() { Text = "had been laid", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar12 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar12 });

                //db.Questions.Add(questionB2grammar13);
                db.Answers.Add(new Answer() { Text = "had known", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2grammar13 });
                db.Answers.Add(new Answer() { Text = "knew", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar13 });
                db.Answers.Add(new Answer() { Text = "have known", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar13 });
                db.Answers.Add(new Answer() { Text = "known", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar13 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar13 });

                //db.Questions.Add(questionB2grammar14);
                db.Answers.Add(new Answer() { Text = "does this necklace costs", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar14 });
                db.Answers.Add(new Answer() { Text = "this necklace costs", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2grammar14 });
                db.Answers.Add(new Answer() { Text = "does this necklace cost", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar14 });
                db.Answers.Add(new Answer() { Text = "this necklace does cost", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar14 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar14 });

                //db.Questions.Add(questionB2grammar15);
                db.Answers.Add(new Answer() { Text = "was investigated", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar15 });
                db.Answers.Add(new Answer() { Text = "was investigating", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar15 });
                db.Answers.Add(new Answer() { Text = "was being investigated", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2grammar15 });
                db.Answers.Add(new Answer() { Text = "was been investigated", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar15 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar15 });

                //db.Questions.Add(questionB2grammar16);
                db.Answers.Add(new Answer() { Text = "won't be", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar16 });
                db.Answers.Add(new Answer() { Text = "won't have been", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar16 });
                db.Answers.Add(new Answer() { Text = "wouldn't have been", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar16 });
                db.Answers.Add(new Answer() { Text = "wouldn't be", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2grammar16 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar16 });

                //db.Questions.Add(questionB2grammar17);
                db.Answers.Add(new Answer() { Text = "didn't use to living", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar17 });
                db.Answers.Add(new Answer() { Text = "wasn't used to living", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2grammar17 });
                db.Answers.Add(new Answer() { Text = "wouldn't live", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar17 });
                db.Answers.Add(new Answer() { Text = "wasn't used to live", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar17 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2grammar17 });





                db.SaveChanges();
                #endregion

                #region B2+
                var questionSetB2plusVocabulary1 = new QuestionSet() { Text = "<p><p>1. Complete the sentences with one word made from the word in brackets.</p> <p>Example: When I began cycling, I found the flat roads easy but the hills almost _________ . (possible) </p> </p>", SkillType = SkillType.Vocabulary, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB2Plus };
                var questionB2plusVocabulary1 = new InputAnswerQuestion() { Text = "<p>1 I []the word so nobody understood me. (pronounce)</p> <p>2 These cups are [], even if you drop them on a hard floor. (break)</p> <p>3 []is a big problem in this area. (vandal)</p> <p>4 The weather here is so [], it could be very different later. (change)</p> <p>5 I like green vegetables, [] spinach. (special)</p> <p>6 Housing costs are high in the capital, so there are a lot of [] people. (home)</p> <p>7 I don’t suffer from []– I enjoy being on my own. (lonely)</p> <p>8 She behaves like a little girl sometimes – she’s so []. (mature)</p> <p>9 I felt very [] when I couldn’t remember her name. (embarrass)</p> <p>10 I’m [] to peanuts, but I can eat any other kinds of nuts. (allergy)</p>", QuestionSet = questionSetB2plusVocabulary1 };

                var questionSetB2plusReading1 = new QuestionSet() { Text = "READING", SkillType = SkillType.Reading, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB2Plus };
                var questionB2plusReading1 = new InputAnswerQuestion() { Text = "<p><b>1. You are going to read an artice about videogames. Seven sentences have been removed from the article. Choose from sentences A-H the one which fits best each gap (1-7). There is one extra sentence which you do not need to use.</b></p> <p>Films and videogames</p> <p>In just a few decades the videogame industry has become a lot bigger than the film business. In terms of turnover, what is rather grandly called 'interactive entertainment' makes twice as much money as Hollywood cinema. Which, of course, leaves people in the film business wondering if they can harvest any of this new income. Is there any way of making films more appealing to people who play videogames?</p> <p>Making a film out of a best-selling videogame can certainly guarantee a large audience. (1)[]. New videogames have stunning action sequences that rely on fantasy effects, and now films are being released with similar scenes. Gravity is discarded as heroes leap across huge gaps, while slow-motion techniques show bullets moving through the rippling air.</p> <p>A major segment of the videogame market comprises sciencce-fiction games, and film-makers have started to realise they could set films in similar sci-fi future worlds. (2)[]. Any attempt to borrow more than the setting from a videogame is probably doomed.</p> <p>There are many examples of succesful film-videogame combinations. Rather than making a film using characters and stories from a videogame, the trick seems to be to make a film that has a fast-moving action sequence and then bring out a videogame based on that sequence. People who enjoyed the film will probably want to buy the videogame. (3)[].</p> <p>Why do game-players feel disappointed by films based on their favourite games? (4)[]. Videogames can show the action from a number of perspectives easily, because everything is computer generated. But filming a sequence from twenty different cameras would cost a fortune, so it simply isn't done in the film version – leaving the game players feeling that the film didn't look as real as the videogame.</p> <p>Cameras matter in another sense, too. In a film the director shows you the action from certain perspectives but makes sure he doesn't show you some things to keep you in suspense. Think of your favourite thriller. (5)[]. In films you are not supposed to have access to all the information. Suspense and mystery are essential elements of film-making.</p> <p>(6)[]. When you play a game, you have to do certain tasks to proceed to the next level. Therefore, you must be able to see everything in order to make your choices, to decide what to do next: which door to open, and so on. You must have access to all the information. You, as the player, are always in control. In the cinema, you never control the action. You just sit and watch.</p> <p>There can be some interaction between films and videogames on a number of different levels, but in the end they fulfill different needs. (7)[]. For all the similarities between technologies and special effects, we shouldn't forget that a story and a game are fundamentally different.</p> <p>A This clearly creates a new market opportunity for the videogame industry.</p> <p>B We go to the cinema to let someone else tell us a story, knowing we can't influence what happens at all.</p> <p>C You wouldn't be interested in watching the film if you knew the identity of the murderer, for instance.</p> <p>D This is not true for videogames.</p> <p>E Its success lies in the use of special effects.</p> <p>F This usually means that the film has a good chance of being as commercially successful as the videogame on which it is based.</p> <p>G One reason is technical.</p> <p>H However, the difficulty for the producers of Hollywood appears to be knowing where and when to stop.</p> ", QuestionSet = questionSetB2plusReading1 };

                var questionSetB2plusGrammar1 = new QuestionSet() { Text = "<p>Complete the second sentence so that it has as similar meaning to the first</p> <p>sentence as possible, using the word given. Do not change the word given. You must use between two and five words, including the word given. Here is an example (0).</p> <p>Example:</p> <p>0 A very friendly taxi driver drove us into town.</p> <p>DRIVEN</p> <p>We were driven into town by a very friendly taxi driver.</p> ", SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizB2Plus };
                var questionB2plusGrammar1 = new InputAnswerQuestion() { Text = "<p>1. 'I'm sorry I'm late again,' he said.</p> <p><b>APOLOGISED</b></p> <p>He [] again.</p> <p>2. She looks like my cousin Mary.</p> <p><b>REMINDS</b></p> <p>She [] my cousin Mary.</p> <p>3. If you have enough money for the fare, why not travel first class?</p> <p><b>AFFORD</b></p> <p>If you [], why not travel first class?</p> <p><br /> 4. Someone is going to redecorate the kitchen for us next month.</p> <p><b>HAVE</b></p> <p>We are going []next month.</p> <p>5. Could you speak up because I can't hear you properly?</p> <p><b>MIND</b></p> <p>Would []up because I can't hear you properly?</p> <p>6. My grandmother became deaf when she was about 60.</p> <p><b>SINCE</b></p> <p>My grandmother []she was about 60.</p> <p>7. We advise customers to buy their tickets in advance.</p> <p><b>ADVISED</b></p> <p>Customers []their tickets in advance.</p> <p>8. It's such a pity I didn't see that film on television last night.</p> <p><b>WISH</b></p> <p>I []that film on television last night.</p> ", QuestionSet = questionSetB2plusGrammar1};

                db.QuestionSets.Add(questionSetB2plusVocabulary1);
                db.QuestionSets.Add(questionSetB2plusReading1);
                db.QuestionSets.Add(questionSetB2plusGrammar1);
                db.SaveChanges();

                db.Questions.Add(questionB2plusVocabulary1);
                db.Questions.Add(questionB2plusReading1);
                db.Questions.Add(questionB2plusGrammar1);

                db.SaveChanges();

                db.Answers.Add(new Answer() { Text = "mispronounced", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2plusVocabulary1 });
                db.Answers.Add(new Answer() { Text = "unbreakable", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2plusVocabulary1 });
                db.Answers.Add(new Answer() { Text = "vandalism", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2plusVocabulary1 });
                db.Answers.Add(new Answer() { Text = "changeable", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2plusVocabulary1 });
                db.Answers.Add(new Answer() { Text = "especially", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2plusVocabulary1 });
                db.Answers.Add(new Answer() { Text = "homeless", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2plusVocabulary1 });
                db.Answers.Add(new Answer() { Text = "loneliness", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2plusVocabulary1 });
                db.Answers.Add(new Answer() { Text = "immature", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2plusVocabulary1 });
                db.Answers.Add(new Answer() { Text = "embarrassed", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2plusVocabulary1 });
                db.Answers.Add(new Answer() { Text = "allergic", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2plusVocabulary1 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2plusVocabulary1 });


                db.Answers.Add(new Answer() { Text = "E", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2plusReading1 });
                db.Answers.Add(new Answer() { Text = "H", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2plusReading1 });
                db.Answers.Add(new Answer() { Text = "A", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2plusReading1 });
                db.Answers.Add(new Answer() { Text = "G", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2plusReading1 });
                db.Answers.Add(new Answer() { Text = "C", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2plusReading1 });
                db.Answers.Add(new Answer() { Text = "D", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2plusReading1 });
                db.Answers.Add(new Answer() { Text = "B", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2plusReading1 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2plusReading1 });

                db.Answers.Add(new Answer() { Text = "apologised for being late", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2plusGrammar1 });
                db.Answers.Add(new Answer() { Text = "reminds me of", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2plusGrammar1 });
                db.Answers.Add(new Answer() { Text = "can afford the fare", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2plusGrammar1 });
                db.Answers.Add(new Answer() { Text = "have the kitchen redecorated|have our kitchen redecorated", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2plusGrammar1 });
                db.Answers.Add(new Answer() { Text = "you mind speaking", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2plusGrammar1 });
                db.Answers.Add(new Answer() { Text = "has been deaf since", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2plusGrammar1 });
                db.Answers.Add(new Answer() { Text = "are advised to buy", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2plusGrammar1 });
                db.Answers.Add(new Answer() { Text = "wish I had seen|wish I had watched", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionB2plusGrammar1 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionB2plusGrammar1 });


                db.SaveChanges();

                #endregion
                
                #region C1

                var questionSetC1Grammar1 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizC1 };
                var questionC1grammar1 = new OneAnswerQuestion() { Text = "What annoys me more than anything else ___________________ loudly during the film at the cinema. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetC1Grammar1 };

                var questionSetC1Grammar2 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizC1 };
                var questionC1grammar2 = new OneAnswerQuestion() { Text = "___________________ his timely intervention, everything would have been lost. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetC1Grammar2 };

                var questionSetC1Grammar3 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizC1 };
                var questionC1grammar3 = new OneAnswerQuestion() { Text = "___________________one problem than they were befallen by another one. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetC1Grammar3 };

                var questionSetC1Grammar4 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizC1 };
                var questionC1grammar4 = new OneAnswerQuestion() { Text = "Little___________________ at the time, how life-changing this experience would prove. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetC1Grammar4 };

                var questionSetC1Grammar5 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizC1 };
                var questionC1grammar5 = new OneAnswerQuestion() { Text = "___________________ part in such an event before, I knew exactly what to expect. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetC1Grammar5 };

                var questionSetC1Grammar6 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizC1 };
                var questionC1grammar6 = new OneAnswerQuestion() { Text = "Nobody seems to know where___________________ . <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetC1Grammar6 };

                var questionSetC1Grammar7 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizC1 };
                var questionC1grammar7 = new OneAnswerQuestion() { Text = "I  remember he mentioned __________________ down his restaurant. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetC1Grammar7 };

                var questionSetC1Grammar8 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizC1 };
                var questionC1grammar8 = new OneAnswerQuestion() { Text = "I___________________ next week by professional decorators. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetC1Grammar8 };

                var questionSetC1Grammar9 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizC1 };
                var questionC1grammar9 = new OneAnswerQuestion() { Text = "We don't stand a chance of winning the match _________________ we make every possible effort! <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetC1Grammar9 };

                var questionSetC1Grammar10 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizC1 };
                var questionC1grammar10 = new OneAnswerQuestion() { Text = "This is the first time I_________________ such outrageous behaviour.. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetC1Grammar10 };

                var questionSetC1Grammar11 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizC1 };
                var questionC1grammar11 = new OneAnswerQuestion() { Text = "If you have trouble going to sleep, try  _________________ a glass of milk before bedtime. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetC1Grammar11 };

                var questionSetC1Grammar12 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizC1 };
                var questionC1grammar12 = new OneAnswerQuestion() { Text = "Which of these sentences is NOT considered correct in spoken (colloquial) English? <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetC1Grammar12 };

                var questionSetC1Grammar13 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizC1 };
                var questionC1grammar13 = new OneAnswerQuestion() { Text = "John Hastings,  _________________ , has just come to live in our street. <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetC1Grammar13 };

                var questionSetC1Grammar14 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizC1 };
                var questionC1grammar14 = new OneAnswerQuestion() { Text = "It's high time you _________________ the truth! <p>[]</p> <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetC1Grammar14 };

                var questionSetC1Grammar15 = new QuestionSet() { SkillType = SkillType.Grammar, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizC1 };
                var questionC1grammar15 = new OneAnswerQuestion() { Text = "The president  is reported_________________ the scene of the attack and to be out of danger. <p>[]</p> <p>[]</p> <p>[]</p>", QuestionSet = questionSetC1Grammar15 };

                var questionSetC1Reading1 = new QuestionSet() { SkillType = SkillType.Reading, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizC1 };
                var questionC1Reading1 = new InputAnswerQuestion() {Text = "<p><b>Reading</b></p> <p><b>1 For questions 1-8, read the text below and think of the word which best fits each gap.</b></p> <p><b> Use ONLY ONE WORD in each gap.</b></p> <p>Handwriting</p> <p>About six months ago, I realised I had no idea what the handwriting of a good friend of mine looked like. We had always communicated by email and text but never by a handwritten letter. And it struck me that we are at a moment [] handwriting seems to be about to vanish from our lives altogether. [] some point in recent years, it stopped [] a necessary and inevitable intermediary between people - a means by [] individuals communicate with each other putting a little bit of their personality [] the form of the message as they press the ink-bearing point onto the paper. lt has started to become just [] among many options, often considered unattractive and elaborate.</p> <p>For each of us, the act of putting marks on paper with ink goes back as [] as we can remember. Our handwriting, like ourselves, seems always to have been there. But now, given that most of us communicate via email and text, have we lost [] crucial to the human experience?</p>", QuestionSet=questionSetC1Reading1 };

                var questionSetC1Reading2 = new QuestionSet() { SkillType = SkillType.Reading, AnswerTime = new TimeSpan(0, 1, 5), Quiz = quizC1 };
                var questionC1Reading2 = new InputAnswerQuestion() { Text = "<p><b>2 You are going to read an article in which four people talk about careers in archaeology. For questions 1 - 7, choose from the people (A-D).</b></p> <p>Which person...</p> <p>1. []suggests that archaeology has a unique appeal?</p> <p>2. [] describes how mutually supportive archaeorogists tend to be?</p> <p>3. [] criticises people who advise against studying archaeology?</p> <p>4. [] welcomes the media profile that archaeology now has?</p> <p>5. [] points out that jobs in archaeology can often be short term?</p> <p>6. [] mentions the value of an archaeorogicar perspective on wider issues?</p> <p>7. [] mentions the appeal of studying a subject with a practicar side to it?</p> <p>Careers in archaeology</p> <p><b>A Jack Stone from The Archaeological Association</b></p> <p>The visibility of archaeology on TV and in the press has increased enormously in recent years. Whether this makes it an attractive career, given an economic climate in which young people understandably favour jobs with good salaries - not common in archaeology - is debatable, but generally, it's had a positive impact. Many archaeologists are hired by small companies to work on excavations; these jobs are often interesting but don't tend to last more than a few months at a time. Then, there are those who work for government organisations, caring for the historical environment. These jobs are more stable, but there are fewer of them. Some people stay on at university doing research and teaching, and others do museum work. ln my experience, most people go into archaeology with their feet firmly on the ground.</p> <p><b>B Dr PaulSimpson, university lecturer</b></p> <p>It's probably what they see on film and TV, but many people assume that archaeology equals digging big holes. While this is obviously an aspect of our work, the bulk of what we do nowadays is lab-based. Few university programmes cover the ground archaeology does. Spanning sciences and humanities, it requires all sorts of skills, and in my department at least, we teach everything from human evolution to the industrial revolution. The number of people wanting to study archaeology is regrettably small - tiny relative to history for example. Potential salaries partly explain this, but it's also down to misguided school teachers saying, 'Why not choose a safe subject like business?' Perhaps they forget it's perfectly feasible to study archaeology and then succeed in an unrelated career. Having said this, half the final-year students in my department stay in archaeology, and tend to be obsessive about it. There's something about telling stories based on evidence you've discovered - and knowing that if you hadn't discovered it, no-one would have – that cannot be experienced in any other field.</p> <p><b>C Victoria Walker, postgraduate student</b></p> <p>l'm researching links between Roman civilisation and lreland 2,000 years ago, which I realise nonarchaeologists might think somewhat obscure. I have a fantastic bunch of academics and students backing me up and there's a tremendous sense of being in it together. lt's a challenging discipline, and one that because of the fieldwork particularly suits a hands-on person like me. Archaeology's wonderful even if you end up doing a completely different kind of job. With hindsight, I now see that the undergraduate course is as much about learning how to do things that can be used in other areas of life, like how to gather and interpret evidence, as it is about archaeology itself.</p> <p><b>D Mark Anderson, field archaeologist</b></p> <p>My company excavates sites before big construction projects like roads and shopping centres get stated on them. Some remains date back many thousands of years, others a couple of centuries; they might be castles, temples, small houses or even just ancient farmland. Over the years, however, l've worked extensively on wetland sites like marshes and river estuaries. This means I have unusual expertise and am in demand for digs in such locations. Much of our work is practical, but we also use imagination to figure out what the tiny fragments we dig up might mean. This, I feel, is something historians, with their access to masses of evidence, tend to miss out on. People say archaeology is a luxury - today's world has far greater problems to solve than investigating how ancient people lived. lt's hard to argue with this, but our troubled globe is run by people seeking quick, short-term solutions, and a deeper, longer-term understanding of humanity's history derived from archaeology, would surely enhance their thinking.</p>", QuestionSet = questionSetC1Reading2 };

                db.QuestionSets.Add(questionSetC1Grammar1);
                db.QuestionSets.Add(questionSetC1Grammar2);
                db.QuestionSets.Add(questionSetC1Grammar3);
                db.QuestionSets.Add(questionSetC1Grammar4);
                db.QuestionSets.Add(questionSetC1Grammar5);
                db.QuestionSets.Add(questionSetC1Grammar6);
                db.QuestionSets.Add(questionSetC1Grammar7);
                db.QuestionSets.Add(questionSetC1Grammar8);
                db.QuestionSets.Add(questionSetC1Grammar9);
                db.QuestionSets.Add(questionSetC1Grammar10);
                db.QuestionSets.Add(questionSetC1Grammar11);
                db.QuestionSets.Add(questionSetC1Grammar12);
                db.QuestionSets.Add(questionSetC1Grammar13);
                db.QuestionSets.Add(questionSetC1Grammar14);
                db.QuestionSets.Add(questionSetC1Grammar15);
                db.QuestionSets.Add(questionSetC1Reading1);
                db.QuestionSets.Add(questionSetC1Reading2);

                db.Questions.Add(questionC1grammar1);
                db.Questions.Add(questionC1grammar2);
                db.Questions.Add(questionC1grammar3);
                db.Questions.Add(questionC1grammar4);
                db.Questions.Add(questionC1grammar5);
                db.Questions.Add(questionC1grammar6);
                db.Questions.Add(questionC1grammar7);
                db.Questions.Add(questionC1grammar8);
                db.Questions.Add(questionC1grammar9);
                db.Questions.Add(questionC1grammar10);
                db.Questions.Add(questionC1grammar11);
                db.Questions.Add(questionC1grammar12);
                db.Questions.Add(questionC1grammar13);
                db.Questions.Add(questionC1grammar14);
                db.Questions.Add(questionC1grammar15);
                db.Questions.Add(questionC1Reading1);
                db.Questions.Add(questionC1Reading2);

                db.SaveChanges();

                // questionC1grammar1
                db.Answers.Add(new Answer() { Text = "is talking people", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar1 });
                db.Answers.Add(new Answer() { Text = "is people to talk", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar1 });
                db.Answers.Add(new Answer() { Text = "is people talking", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1grammar1 });
                db.Answers.Add(new Answer() { Text = "people talking", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar1 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar1 });

                // questionC1grammar2
                db.Answers.Add(new Answer() { Text = "Had it been", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar2 });
                db.Answers.Add(new Answer() { Text = "Had it not been for", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1grammar2 });
                db.Answers.Add(new Answer() { Text = "Was it not for", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar2 });
                db.Answers.Add(new Answer() { Text = "Were it not", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar2 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar2 });

                //db.Questions.Add(questionC1grammar3);
                db.Answers.Add(new Answer() { Text = "No sooner had they resolved", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1grammar3 });
                db.Answers.Add(new Answer() { Text = "No sooner did they resolve", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar3 });
                db.Answers.Add(new Answer() { Text = "No sooner they resolved", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar3 });
                db.Answers.Add(new Answer() { Text = "No sooner did they resolved", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar3 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar3 });

                //db.Questions.Add(questionC1grammar4);
                db.Answers.Add(new Answer() { Text = "did we realise", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1grammar4 });
                db.Answers.Add(new Answer() { Text = "we  did realise", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar4 });
                db.Answers.Add(new Answer() { Text = "we realised", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar4 });
                db.Answers.Add(new Answer() { Text = "realised we", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar4 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar4 });

                //db.Questions.Add(questionC1grammar5);
                db.Answers.Add(new Answer() { Text = "Taking", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar5 });
                db.Answers.Add(new Answer() { Text = "Having taken", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1grammar5 });
                db.Answers.Add(new Answer() { Text = "Having been taken", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar5 });
                db.Answers.Add(new Answer() { Text = "Being taken", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar5 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar5 });

                //db.Questions.Add(questionC1grammar6);
                db.Answers.Add(new Answer() { Text = "the rumour did originate from", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar6 });
                db.Answers.Add(new Answer() { Text = "did the rumour originate from", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar6 });
                db.Answers.Add(new Answer() { Text = "the rumour originate from", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar6 });
                db.Answers.Add(new Answer() { Text = "the rumour originated from", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1grammar6 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar6 });

                //db.Questions.Add(questionC1grammar7);
                db.Answers.Add(new Answer() { Text = "to close", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar7 });
                db.Answers.Add(new Answer() { Text = "closing", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1grammar7 });
                db.Answers.Add(new Answer() { Text = "close", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar7 });
                db.Answers.Add(new Answer() { Text = "to closing", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar7 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar7 });

                //db.Questions.Add(questionC1grammar8);
                db.Answers.Add(new Answer() { Text = "am renovating my flat", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar8 });
                db.Answers.Add(new Answer() { Text = "have renovated my flat", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar8 });
                db.Answers.Add(new Answer() { Text = "have my flat renovated", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar8 });
                db.Answers.Add(new Answer() { Text = "am having my flat renovated", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1grammar8 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar8 });

                //db.Questions.Add(questionC1grammar9);
                db.Answers.Add(new Answer() { Text = "if", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar9 });
                db.Answers.Add(new Answer() { Text = "providing that", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar9 });
                db.Answers.Add(new Answer() { Text = "except", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar9 });
                db.Answers.Add(new Answer() { Text = "unless", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1grammar9 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar9 });

                //db.Questions.Add(questionC1grammar10);
                db.Answers.Add(new Answer() { Text = "witnessed", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar10 });
                db.Answers.Add(new Answer() { Text = "have witnessed", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar10 });
                db.Answers.Add(new Answer() { Text = "witness", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar10 });
                db.Answers.Add(new Answer() { Text = "am witnessing", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1grammar10 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar10 });

                //db.Questions.Add(questionC1grammar11);
                db.Answers.Add(new Answer() { Text = "drinking", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1grammar11 });
                db.Answers.Add(new Answer() { Text = "to drink", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar11 });
                db.Answers.Add(new Answer() { Text = "drink", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar11 });
                db.Answers.Add(new Answer() { Text = "to have drunk", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar11 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar11 });

                //db.Questions.Add(questionC1grammar12);
                db.Answers.Add(new Answer() { Text = "Car's running badly.", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar12 });
                db.Answers.Add(new Answer() { Text = "Have heard of her.", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1grammar12 });
                db.Answers.Add(new Answer() { Text = "Careful what you say.", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar12 });
                db.Answers.Add(new Answer() { Text = "Seen Peter?", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar12 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar12 });

                //db.Questions.Add(questionC1grammar13);
                db.Answers.Add(new Answer() { Text = "that I was at school with", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar13 });
                db.Answers.Add(new Answer() { Text = "with who I was at school", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar13 });
                db.Answers.Add(new Answer() { Text = "I was at school with", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar13 });
                db.Answers.Add(new Answer() { Text = "with whom I was at school", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1grammar13 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar13 });

                //db.Questions.Add(questionC1grammar14);
                db.Answers.Add(new Answer() { Text = "us to take", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar14 });
                db.Answers.Add(new Answer() { Text = "to take", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar14 });
                db.Answers.Add(new Answer() { Text = "taking", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar14 });
                db.Answers.Add(new Answer() { Text = "us taking", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1grammar14 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar14 });

                //db.Questions.Add(questionC1grammar15);
                db.Answers.Add(new Answer() { Text = "tell", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar15 });
                db.Answers.Add(new Answer() { Text = "are telling", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar15 });
                db.Answers.Add(new Answer() { Text = "have told", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar15 });
                db.Answers.Add(new Answer() { Text = "told", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1grammar15 });
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1grammar15 });

                //qC1reading1
                db.Answers.Add(new Answer() { Text = "when", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1Reading1});
                db.Answers.Add(new Answer() { Text = "at", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1Reading1});
                db.Answers.Add(new Answer() { Text = "being", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1Reading1});
                db.Answers.Add(new Answer() { Text = "which", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1Reading1});
                db.Answers.Add(new Answer() { Text = "into", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1Reading1});
                db.Answers.Add(new Answer() { Text = "one", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1Reading1});
                db.Answers.Add(new Answer() { Text = "far", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1Reading1});
                db.Answers.Add(new Answer() { Text = "something", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1Reading1});
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1Reading1});

                //C1reading2

                db.Answers.Add(new Answer() { Text = "B", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1Reading2});
                db.Answers.Add(new Answer() { Text = "C", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1Reading2});
                db.Answers.Add(new Answer() { Text = "B", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1Reading2});
                db.Answers.Add(new Answer() { Text = "A", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1Reading2});
                db.Answers.Add(new Answer() { Text = "A", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1Reading2});
                db.Answers.Add(new Answer() { Text = "D", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1Reading2});
                db.Answers.Add(new Answer() { Text = "C", IsCorrect = true, Points = 4, NegativePoints = 0, Question = questionC1Reading2});
                db.Answers.Add(new Answer() { Text = "EmptyAnswer", IsCorrect = false, Points = 0, NegativePoints = 0, Question = questionC1Reading2});


                db.SaveChanges();
                    
            #endregion 
            }
        }

        private void AssignUsersAndRoles(BritanicaQuizDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "admin"))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole { Name = "admin" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "pesho@pesho.com"))
            {
                var store = new UserStore<User>(context);
                var manager = new UserManager<User>(store);
                var adminUser = new User { UserName = "pesho@pesho.com", Email = "pesho@pesho.com", EmailConfirmed = true };

                manager.Create(adminUser, "peshopesho");
                manager.AddToRole(adminUser.Id, "admin");

                var regularUser = new User { UserName = "misho@misho.com", Email = "misho@misho.com", EmailConfirmed = true };
                manager.Create(regularUser, "mishomisho");
            }
        }
    }
}
