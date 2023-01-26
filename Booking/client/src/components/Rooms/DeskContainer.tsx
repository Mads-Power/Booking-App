import { UseQueryResult } from "@tanstack/react-query";
import { Booking } from "@type/booking";
import { Room } from "@type/room"
import { User } from "@type/user";
import { DeskRender } from "./DeskRender";
import { RoomPickerLargeScreens } from "./RoomPicker-LargeScreens";

type TDeskContainer = {
    data: Room[];
    selectedRoomFromParent: Room;
    occupiedSeats: UseQueryResult<Booking[], unknown>;
    user: User;
    setSelectedRoom: (roomId: number) => void;
}

export const DeskContainer = ({ data, selectedRoomFromParent, occupiedSeats, user, setSelectedRoom }: TDeskContainer) => {

    const handleSelectRoomFromChild = (roomId: number) => {
        setSelectedRoom(roomId)
    }

    return (
        <div className="w-[90%] lg:w-[80%] lg:flex lg:flex-row lg:justify-around lg:child:mx-2 grow child:h-[80%] mx-auto">
            {/* This div will be hidden on all screens smaller than 1024px */}
            {/* Roompicker for bigger screens */}
            <RoomPickerLargeScreens
                rooms={data}
                selectedRoomFromParent={selectedRoomFromParent}
                setSelectedRoom={handleSelectRoomFromChild}
            />

            <div className="lg:basis-3/4 lg:w-[90%] bg-slate-400 bg-opacity-10 ">
                {selectedRoomFromParent ? (
                    <DeskRender
                        selectedRoomId={selectedRoomFromParent.id}
                        occupiedSeats={occupiedSeats}
                        user={user}
                    />
                ) : (<>Kunne ikke finne valgt rom</>)}
            </div>
        </div>
    )
}