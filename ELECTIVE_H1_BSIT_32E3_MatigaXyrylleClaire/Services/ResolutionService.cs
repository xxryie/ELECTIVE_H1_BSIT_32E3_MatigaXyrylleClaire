using ELECTIVE_H1_BSIT_32E3_MatigaXyrylleClaire.Models;

namespace ELECTIVE_H1_BSIT_32E3_MatigaXyrylleClaire.Services
{
    public class ResolutionService
    {
        private static List<Resolution> _resolutions = new()
        {
            new Resolution
            {
                Id = 1,
                Title = "Maintain dean's lister status this semester",
                IsDone = false,
                CreatedAt = DateTime.UtcNow.AddDays(-5)
            },
            new Resolution
            {
                Id = 2,
                Title = "Stop coding at 3 AM just to finish projects",
                IsDone = false,
                CreatedAt = DateTime.UtcNow.AddDays(-4)
            },
            new Resolution
            {
                Id = 3,
                Title = "Finish all pending programming projects on time",
                IsDone = true,
                CreatedAt = DateTime.UtcNow.AddDays(-3),
                UpdatedAt = DateTime.UtcNow.AddDays(-1)
            },
             new Resolution
            {
                Id = 4,
                Title = "Learn a new programming language (not love language)",
                IsDone = false,
                CreatedAt = DateTime.UtcNow.AddDays(-9)
            },
            new Resolution
            {
                Id = 5,
                Title = "Build a portfolio website (not a shrine for crush)",
                IsDone = false,
                CreatedAt = DateTime.UtcNow.AddDays(-8)
            },
        };
        private static int _nextId = 5;

        public List<Resolution> GetAll(bool? isDone, string? title)
        {
            var query = _resolutions.AsEnumerable();

            if (isDone.HasValue)
            {
                query = query.Where(r => r.IsDone == isDone.Value);
            }

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(r => r.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            }

            return query.ToList();
        }

        public Resolution? GetById(int id)
        {
            return _resolutions.FirstOrDefault(r => r.Id == id);
        }

        public Resolution Create(string title)
        {
            var resolution = new Resolution
            {
                Id = _nextId++,
                Title = title,
                IsDone = false,
                CreatedAt = DateTime.UtcNow
            };

            _resolutions.Add(resolution);
            return resolution;
        }

        public Resolution? Update(int id, UpdateResolutionDto dto)
        {
            var resolution = _resolutions.FirstOrDefault(r => r.Id == id);
            if (resolution == null)
                return null;

            resolution.Title = dto.Title;
            resolution.IsDone = dto.IsDone;
            resolution.UpdatedAt = DateTime.UtcNow;

            return resolution;
        }

        public bool Delete(int id)
        {
            var resolution = _resolutions.FirstOrDefault(r => r.Id == id);
            if (resolution == null)
                return false;

            _resolutions.Remove(resolution);
            return true;
        }
    }
}
