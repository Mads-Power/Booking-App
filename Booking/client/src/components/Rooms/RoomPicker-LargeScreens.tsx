import { Room } from "@type/room"


type TRoomPickerLargeScreens = {
    rooms: Room[];
    selectedRoomFromParent: Room;
    setSelectedRoom: (roomId: number) => void
}

export const RoomPickerLargeScreens = ({rooms, selectedRoomFromParent, setSelectedRoom}: TRoomPickerLargeScreens) => {

    const handleSelectRoom = (roomId: number) => {
        setSelectedRoom(roomId)
    }

    const renderMenuButtonForLargeScreens = (room: Room) => {
        return <div className={"w-full bg-opacity-20 flex flex-row shadow-md mx-auto justify-between " + (selectedRoomFromParent?.id === room.id ? 'bg-slate-800' : 'bg-slate-400')}>
            <div className="p-2 m-2">
                <p className="text-base">{room.name}</p>
            </div>
            <div
                className="basis-1/4 h-full flex align-middle bg-slate-400 bg-opacity-10">
                <svg xmlns="http://www.w3.org/2000/svg"
                    height="20"
                    width="20"
                    className="mx-auto self-center"
                ><path d="M9.4 18 8 16.6l4.6-4.6L8 7.4 9.4 6l6 6Z" /></svg>
            </div>
        </div>
    }


    return (
        <div className="hidden lg:block basis-1/4 bg-slate-400 bg-opacity-10 p-2 overflow-hidden">
            <div className="w-full flex flex-col h-full gap-y-4">
                {rooms.map((room) => (
                    <div
                        key={room.id}
                        className="w-full flex hover:cursor-pointer"
                        onClick={() => handleSelectRoom(room.id)}>
                        {renderMenuButtonForLargeScreens(room)}
                    </div>
                ))}
            </div>
        </div>
    )
}