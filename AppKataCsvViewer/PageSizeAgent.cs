using System;

namespace AppKataCsvViewer
{
    public class PageSizeAgent
    {
        private readonly int defaultPageSize;
        private readonly int indexOfPageSize;

        public PageSizeAgent(int defaultPageSize, int indexOfPageSize)
        {
            if (defaultPageSize < 1)
                throw new DefaultPageSizeMustBeHigherThanZero();
            
            if (indexOfPageSize < 0)
                throw new NegativeIndexOfCustomPageSizeNotAllowed();

            this.defaultPageSize = defaultPageSize;
            this.indexOfPageSize = indexOfPageSize;
        }

        public virtual int DetectPageSize(string[] args)
        {
            if (args == null || NoIndexForCustomPageSizeWasGiven(args))
                return defaultPageSize;

            int pageSize;

            if (Int32.TryParse(args[indexOfPageSize], out pageSize))
                return pageSize;

            return defaultPageSize;
        }

        private bool NoIndexForCustomPageSizeWasGiven(string[] args)
        {
            return args.Length < (indexOfPageSize + 1);
        }

        public class DefaultPageSizeMustBeHigherThanZero : Exception
        {
        }

        public class NegativeIndexOfCustomPageSizeNotAllowed : Exception
        {
        }
    }
}