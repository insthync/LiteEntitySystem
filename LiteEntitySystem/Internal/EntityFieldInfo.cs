﻿namespace LiteEntitySystem.Internal
{
    internal enum FieldType
    {
        SyncVar,
        SyncableSyncVar
    }

    public enum OnSyncExecutionOrder
    {
        BeforeConstruct,
        AfterConstruct
    }

    internal struct EntityFieldInfo
    {
        public readonly string Name; //used for debug
        public readonly ValueTypeProcessor TypeProcessor;
        public readonly int Offset;
        public readonly int SyncableSyncVarOffset;
        public readonly uint Size;
        public readonly int IntSize;
        public readonly FieldType FieldType;
        public readonly SyncFlags Flags;
        public readonly bool IsPredicted;

        public MethodCallDelegate OnSync;
        public OnSyncExecutionOrder OnSyncExecutionOrder;
        public int FixedOffset;
        public int PredictedOffset;

        //for value type
        public EntityFieldInfo(
            string name,
            ValueTypeProcessor valueTypeProcessor,
            int offset,
            SyncFlags flags)
        {
            Name = name;
            TypeProcessor = valueTypeProcessor;
            SyncableSyncVarOffset = -1;
            Offset = offset;
            Size = (uint)TypeProcessor.Size;
            IntSize = TypeProcessor.Size;
            FieldType = FieldType.SyncVar;
            FixedOffset = 0;
            PredictedOffset = 0;
            Flags = flags;
            OnSync = null;
            OnSyncExecutionOrder = OnSyncExecutionOrder.AfterConstruct;
            IsPredicted = Flags.HasFlagFast(SyncFlags.AlwaysRollback) ||
                          (!Flags.HasFlagFast(SyncFlags.OnlyForOtherPlayers) &&
                           !Flags.HasFlagFast(SyncFlags.NeverRollBack));
        }

        //For syncable syncvar
        public EntityFieldInfo(
            string name,
            ValueTypeProcessor valueTypeProcessor,
            int offset,
            int syncableSyncVarOffset,
            SyncFlags flags)
        {
            Name = name;
            TypeProcessor = valueTypeProcessor;
            SyncableSyncVarOffset = syncableSyncVarOffset;
            Offset = offset;
            Size = (uint)TypeProcessor.Size;
            IntSize = TypeProcessor.Size;
            FieldType = FieldType.SyncableSyncVar;
            FixedOffset = 0;
            PredictedOffset = 0;
            Flags = flags;
            OnSync = null;
            OnSyncExecutionOrder = OnSyncExecutionOrder.AfterConstruct;
            IsPredicted = Flags.HasFlagFast(SyncFlags.AlwaysRollback) ||
                          (!Flags.HasFlagFast(SyncFlags.OnlyForOtherPlayers) &&
                           !Flags.HasFlagFast(SyncFlags.NeverRollBack));
        }
    }
}