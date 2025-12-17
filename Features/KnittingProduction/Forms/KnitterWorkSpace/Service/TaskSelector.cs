using SewingProduction.Features.KnittingProduction.Forms.KnitterWorkSpace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWorkSpace.Service
{
    public static class TaskSelector
    {
        // Выбирает подмножество задач так, чтобы
        // суммарное время было максимально, но <= maxHours.
        public static List<TaskCandidate> KnapsackByHours(
            List<TaskCandidate> tasks,
            double maxHours)
        {
            if (tasks == null || tasks.Count == 0 || maxHours <= 0)
                return new List<TaskCandidate>();

            int n = tasks.Count;
            int capacity = (int)Math.Round(maxHours * 60); // работаем в минутах

            int[] weights = tasks
                .Select(t => Math.Max(1, (int)Math.Round(t.Hours * 60))) // хотя бы 1 минута
                .ToArray();

            // dp[c] = максимальное суммарное время (в минутах), которое можно набрать при ёмкости c
            int[] dp = new int[capacity + 1];
            bool[,] take = new bool[n, capacity + 1];

            for (int i = 0; i < n; i++)
            {
                int w = weights[i];
                for (int c = capacity; c >= w; c--)
                {
                    int candidate = dp[c - w] + w;
                    if (candidate > dp[c])
                    {
                        dp[c] = candidate;
                        take[i, c] = true;
                    }
                }
            }

            // Восстановление выбранных задач
            var selected = new List<TaskCandidate>();
            int cur = capacity;

            for (int i = n - 1; i >= 0; i--)
            {
                int w = weights[i];
                if (cur >= w && take[i, cur])
                {
                    selected.Add(tasks[i]);
                    cur -= w;
                }
            }

            selected.Reverse();
            return selected;
        }

        // Основной метод: учитываем приоритеты и лимит часов
        public static List<TaskCandidate> SelectTasksWithPriority(
            IEnumerable<TaskCandidate> allTasks,
            double maxHours)
        {
            var result = new List<TaskCandidate>();
            if (allTasks == null) return result;

            double usedHours = 0.0;

            // Группируем по приоритету и идём от 1 к 3
            var groups = allTasks
                .GroupBy(t => t.PriorityGroup)
                .OrderBy(g => g.Key);

            foreach (var group in groups)
            {
                double remaining = maxHours - usedHours;
                if (remaining <= 0)
                    break;

                var groupList = group.ToList();

                // выбираем из этой группы задачи, не выходя за оставшийся лимит
                var selectedFromGroup = KnapsackByHours(groupList, remaining);

                result.AddRange(selectedFromGroup);
                usedHours += selectedFromGroup.Sum(t => t.Hours);
            }

            return result;
        }
    }
    public sealed record TaskCandidate(
    int PzvId,
    int PriorityGroup,   // 1, 2 или 3
    double Hours         // длительность операции в часах
);
}
