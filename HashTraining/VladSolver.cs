using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HashTraining
{
    public class VladSolver
    {
        public BookScanningOutput Solve(BookScanning model)
        {
            var librariesOrderedByOutput = model.Libraries
               .OrderByDescending(l =>
             {
                 var bookScore = l.Books.Sum(b => model.BookScores[b]);
                 var avgBookScore = bookScore / l.Books.Count;

                 var outputPotential = avgBookScore * l.BooksShippedPerDay;
                 var availableTime = model.NumberOfDays - l.SigningTime;

                 return outputPotential * availableTime;
             }).ToList();

            var usedBooks = new List<int>();
            var signedLibraries = new List<LibraryData>
            {
                new LibraryData
                {
                    Library = librariesOrderedByOutput.First(),
                    SignedBooks = new List<int>(),
                    SignedDay = 0,
                    IsSigning = true
                }
            };

            for (var day = 0; day < model.NumberOfDays; day++)
            {
                Console.WriteLine($"day: {day}");
                Console.WriteLine($"lib: {signedLibraries.Count}");

                for (var i = 0; i < signedLibraries.Count; i++)
                {
                    if (signedLibraries[i].SignedDay + signedLibraries[i].Library.SigningTime > day)
                    {
                        Console.WriteLine($"finished signing day: {day}, {signedLibraries[i].SignedDay + signedLibraries[i].Library.SigningTime}");

                        var newBooks = signedLibraries[i].Library.Books.Where(b => !usedBooks.Contains(b))
                                              .OrderByDescending(b => model.BookScores[b])
                                              .Take(signedLibraries[i].Library.BooksShippedPerDay);

                        signedLibraries[i].IsSigning = false;
                        signedLibraries[i].SignedBooks.AddRange(newBooks);
                        usedBooks.AddRange(newBooks);
                    }

                    if (!signedLibraries[signedLibraries.Count - 1].IsSigning)
                    {
                        Console.WriteLine($"new library day: {day}");

                        signedLibraries.Add(new LibraryData
                        {
                            Library = librariesOrderedByOutput[signedLibraries.Count],
                            SignedBooks = new List<int>(),
                            SignedDay = day,
                            IsSigning = true
                        });
                    }
                }
            }

            return new BookScanningOutput()
            {
                NumberOfScannedLibraries = signedLibraries.Count,
                ScannedLibraries = signedLibraries.Select(s =>
                 (s.Library.Index, s.SignedBooks)).ToList()
            };
        }

        public class LibraryData
        {
            public Library Library { get; set; }
            public int SignedDay { get; set; }
            public List<int> SignedBooks { get; set; }
            public bool IsSigning { get; set; }
        }
    }
}
