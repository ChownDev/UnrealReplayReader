namespace UnrealReplayReader.Models.Enums;

[Flags]
public enum EUniqueIdEncodingFlags
{
    /// <summary>
    /// Default, nothing encoded, use normal FString serialization
    /// </summary>
    NotEncoded = 0,

    /// <summary>
    ///  Data is optimized based on some assumptions (even number of [0-9][a-f][A-F] that can be packed into nibbles
    /// </summary>
    IsEncoded = 1 << 0,

    /// <summary>
    /// This unique id is empty or invalid, nothing further to serialize
    /// </summary>
    IsEmpty = 1 << 1,

    /// <summary>
    /// Reserved for future use
    /// </summary>
    Unused1 = 1 << 2,

    /// <summary>
    /// Remaining bits are used for encoding the type without requiring another byte
    /// </summary>
    Reserved1 = 1 << 3,
    Reserved2 = 1 << 4,
    Reserved3 = 1 << 5,
    Reserved4 = 1 << 6,
    Reserved5 = 1 << 7,

    /// <summary>
    /// Helper masks
    /// </summary>
    FlagsMask = Reserved1 - 1,

    TypeMask = 255 ^ FlagsMask
    // TypeMask = (MAX_uint8 ^ FlagsMask)
}