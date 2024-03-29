﻿namespace Photor.Infrastructure.Data.Constants
{
    public static class DbModelsConstants
    {
        public static class ApplicationUser
        {
            public const int UserNameMinLength = 5;
            public const int UserNameMaxLength = 20;

            public const int EmailMinLength = 10;
            public const int EmailMaxLength = 60;

            public const int PasswordMinLength = 5;
            public const int PasswordMaxLength = 20;

            public const int FirstAndLastNameMinLength = 2;
            public const int FirstAndLastNameMaxLength = 30;

            public const int DescriptionMinLength = 0;
            public const int DescriptionMaxLength = 1000;
        }

        public static class Post
        {
            public const int DescriptionMinLength = 0;
            public const int DescriptionMaxLength = 500;
        }

        public static class UserPostComment
        {
            public const int CommentMinLength = 1;
            public const int CommentMaxLength = 1000;
        }

        public static class UserPostReport
        {
            public const int ReasonMinLength = 10;
            public const int ReasonMaxLength = 1000;
        }
    }
}