﻿namespace Restaurents_API.Exceptions
{
    public class NotFoundException : Exception
    {
        public string ResourceType { get; }
        public string ResourceIdentifier { get; }

        public NotFoundException(string resourceType, string resourceIdentifier)
            : base($"{resourceType} with id: {resourceIdentifier} doesn't exist")
        {
            ResourceType = resourceType;
            ResourceIdentifier = resourceIdentifier;
        }
    }
}
