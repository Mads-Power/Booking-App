import { UseQueryResult } from "@tanstack/react-query";
import { Booking } from "@type/booking";
import { User } from "@type/user";
import { useNavigate } from "react-router-dom";


type TDeskRender = {
    selectedRoomId: number;
    occupiedSeats: UseQueryResult<Booking[], unknown>;
    user: User
}

export const DeskRender = ({ selectedRoomId, occupiedSeats, user }: TDeskRender) => {

    const navigate = useNavigate();

    const renderDeskSvgs = (seatId: number) => {
        return <svg xmlns="http://www.w3.org/2000/svg"
            height="48"
            width="48"
            onClick={() => handleDeskClick(seatId)}
            fill={chooseDeskFill(seatId)}
            className="transition ease-in-out
          lg:h-16 lg:w-16 lg:p-2
          hover:border-solid hover:border hover:shadow-lg hover:cursor-pointer hover:scale-125"
        >
            <path d="M4 36V12h40v24h-3v-5h-9.5v5h-3V15H7v21Zm27.5-16H41v-5h-9.5Zm0 8H41v-5h-9.5Z" />
        </svg>
    }

    const chooseDeskFill = (seatId: number) => {
        let fillHex = "";
        const occupied = occupiedSeats.data?.find(seat => seat.seatId === seatId);
        if (occupied) {
            // If the seat is booked by the current logged in user, set the `fillHex` to be green, if not, set the color to be blue
            occupiedSeats.data?.some(seat => {
                if (seat.userId === user?.id && seat.seatId === seatId) {
                    fillHex = "#68B984";
                    return true;
                }
                else {
                    fillHex = "#3981F1";
                }
            })
        } else {
            // If seat is free, set `fillHex` to the color black
            fillHex = "#000000"
        }
        return fillHex
    }

    const handleDeskClick = (id: number) => {
        navigate(`/seat/${id}`, { relative: "path" })
    }

    //Note: The code for 'renderBigRoom' and 'renderSmallRoom' is not optimal as we need to specifically define where we would like
    // our desks to be placed with a specific ID. If it is to be refactored in the future a svg layout / map of the office space
    // would be both cleaner UI wise as well as code wise, but for now, this is ok.

    const renderBigRoom = () => {
        return (
            <div className="w-full mx-auto child:p-2">
                <div>
                    <div className="flex flex-col">
                        <div className="flex flex-row-reverse gap-x-6">
                            {renderDeskSvgs(1)}
                            {renderDeskSvgs(2)}
                            {renderDeskSvgs(3)}
                        </div>
                        <div className="flex flex-row-reverse gap-x-6">
                            {renderDeskSvgs(4)}
                            {renderDeskSvgs(5)}
                            {renderDeskSvgs(6)}
                        </div>
                    </div>
                </div>
                <div>
                    <div className="p-4 flex flex-col">
                        <div className="flex flex-col">
                            <div className="flex flex-row-reverse gap-x-6">
                                {renderDeskSvgs(7)}
                                {renderDeskSvgs(8)}
                            </div>
                            <div className="flex flex-row-reverse gap-x-6">
                                {renderDeskSvgs(9)}
                                {renderDeskSvgs(10)}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        )
    }

    const renderSmallerRoom = () => {
        return (
            <div className="w-full lg:w-auto mx-auto">
                <div className="p-4 flex flex-col gap-y-4">
                    <div className="flex flex-row-reverse gap-x-6 justify-center">
                        {renderDeskSvgs(11)}
                        {renderDeskSvgs(12)}
                    </div>
                    <div className="flex flex-row justify-end">
                        {renderDeskSvgs(13)}
                    </div>
                    <div className="flex flex-row justify-between">
                        {renderDeskSvgs(14)}
                        {renderDeskSvgs(15)}
                    </div>
                </div >
            </div >
        )
    }

    return (
        <div>
            {selectedRoomId === 1 ? (
                <div>
                    {renderBigRoom()}
                </div>
            ) : (
                <div>{renderSmallerRoom()}</div>
            )}
        </div >
    )
}