namespace snake
{
    public enum SegmentType
    {
        // Основные головы
        HeadUp,
        HeadDown,
        HeadLeft,
        HeadRight,

        // Открытый рот
        HeadUpOpen,
        HeadDownOpen,
        HeadLeftOpen,
        HeadRightOpen,

        // Губы (перед головой)
        LipsUp,
        LipsDown,
        LipsLeft,
        LipsRight,

        // Мертвая голова
        HeadUpDead,
        HeadDownDead,
        HeadLeftDead,
        HeadRightDead,

        // Тело
        BodyVertical,
        BodyHorizontal,
        BodyTopRight, // ┐
        BodyTopLeft,  // ┌
        BodyBottomRight, // ┘
        BodyBottomLeft, // └

        // Хвост
        TailUp,
        TailDown,
        TailLeft,
        TailRight,

        // Язык
        TongueUp,
        TongueDown,
        TongueLeft,
        TongueRight
    }
}