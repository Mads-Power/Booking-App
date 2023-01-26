import { UseQueryResult } from "@tanstack/react-query";
import { Booking } from "@type/booking";
import { Room } from "@type/room"
import { User } from "@type/user";
import { Dispatch, SetStateAction, useEffect } from "react";
import { DeskRender } from "./DeskRender";

type TDeskContainer = {
    data: Room[];
    selectedRoomFromParent: Room;
    occupiedSeats: UseQueryResult<Booking[], unknown>;
    user: User;
    setSelectedRoom: (roomId: number) => void;
}

export const DeskContainer = ({ data, selectedRoomFromParent, occupiedSeats, user, setSelectedRoom }: TDeskContainer) => {

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
        <div className="w-[90%] lg:w-[80%] lg:flex lg:flex-row lg:justify-around lg:child:mx-2 grow child:h-[80%] mx-auto">
            {/* This div will be hidden on all screens smaller than 1024px */}
            {/* Roompicker for bigger screens */}
            <div className="hidden lg:block basis-1/4 bg-slate-400 bg-opacity-10 p-2 overflow-hidden">
                <div className="w-full flex flex-col h-full gap-y-4">
                    {data?.map((room) => (
                        <div
                            key={room.id}
                            className="w-full flex hover:cursor-pointer"
                            onClick={() => handleSelectRoom(room.id)}>
                            {renderMenuButtonForLargeScreens(room)}
                        </div>
                    ))}
                </div>
            </div>
            <div className="lg:basis-3/4 lg:w-[90%] bg-slate-400 bg-opacity-10 ">
                {selectedRoomFromParent ? (
                    <DeskRender selectedRoomId={selectedRoomFromParent.id} occupiedSeats={occupiedSeats} user={user} />
                ) : (<>Kunne ikke finne valgt rom</>)}
            </div>
        </div>
    )
}