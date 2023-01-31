import { Menu, MenuItem } from "@mui/material";
import { Booking, DeleteBooking, TUnbook } from "@type/booking"
import { User } from "@type/user";
import * as dayjs from 'dayjs'
import { Dispatch, SetStateAction, useState } from "react";

type TBookingListItem = {
    user: User;
    booking: Booking;
    onUnbook: (booking: TUnbook) => void;
};

export const BookingsListItem = ({ booking, user, onUnbook }: TBookingListItem) => {
    const room = (booking.seatId >= 10) ? "Storerommet" : "Lillerommet";

    const [contextmenuAnchor, setContextmenuAnchorState] = useState<null | HTMLElement>(null);
    const openContextmenu = Boolean(contextmenuAnchor);
    const handleOpenContextmenu = (event: React.MouseEvent<HTMLParagraphElement>) => {
        setContextmenuAnchorState(event.currentTarget);
    };
    const handleCloseContextmenu = () => {
        setContextmenuAnchorState(null);
    };

    const handleUnbook = () => {
        setContextmenuAnchorState(null);
        const unbookingData = {
            id: booking.id,
            seatId: booking.seatId,
            email: booking.email,
            date: booking.date
        } as TUnbook;
        onUnbook(unbookingData)
    };


    return (
        <div className="shadow border flex flex-row w-[90%] mx-auto lg:w-[75%]">
            <div className="flex flex-col grow p-2">
                <p className="text-xl p-1">
                    {dayjs(booking.date).format("D[.] MMMM YYYY")}
                </p>
                <p className="text-lg">{room}</p>
            </div>
            <div className="flex-none flex">
                <p
                    className="self-end rotate-90 text-2xl hover:cursor-pointer"
                    aria-controls={openContextmenu ? 'basic-menu' : undefined}
                    aria-haspopup="true"
                    aria-expanded={openContextmenu ? 'true' : undefined}
                    onClick={handleOpenContextmenu}
                >...</p>
            </div>

            <Menu
                id="mui-context-menu"
                anchorEl={contextmenuAnchor}
                open={openContextmenu}
                onClose={handleCloseContextmenu}
                MenuListProps={{
                    'aria-labelledby': 'basic-button',
                }}
            >
                <MenuItem
                    onClick={handleUnbook}
                    className="text-[#DF0D0D]"
                >Fjern booking</MenuItem>
            </Menu>
        </div>
    )
}