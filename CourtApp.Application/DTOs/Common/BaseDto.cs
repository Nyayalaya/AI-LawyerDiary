using System;

namespace CourtApp.Application.Dtos.Common
{
    /// <summary>
    /// Base response DTO with common identifier
    /// Used as the foundation for all entity DTOs
    /// </summary>
    public abstract class BaseResponseDto
    {
        /// <summary>
        /// Unique identifier for the entity
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Timestamp when the entity was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Timestamp when the entity was last updated
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Indicates if the entity is soft-deleted
        /// </summary>
        public bool IsDeleted { get; set; }
    }

    /// <summary>
    /// Base request DTO for create/update operations
    /// </summary>
    public abstract class BaseRequestDto
    {
        // Base properties can be added here as needed
    }
}