import { Alert, AlertProps, CircularProgress, Snackbar } from "@mui/material";
import { forwardRef, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useUserQuery } from "../../api/useUserQuery";
import { BookingsListItem } from "./BookingsListItem";
import ArrowCircleLeftIcon from '@mui/icons-material/ArrowCircleLeft';
import dayjs from 'dayjs';

import relativeTime from 'dayjs/plugin/relativeTime';
import { Booking, TUnbook } from "@type/booking";
import { useRemoveBookingMutation } from "@api/useRemoveBookingMutation";
import MuiAlert, { AlertColor } from '@mui/material/Alert';
import { useAtom } from "jotai";
import { userAtom } from "@components/Provider/app";
import { getMe } from "@api/getMe";
import { User } from "@type/user";


dayjs.extend(relativeTime)

export const Bookings = () => {
    const [ user, setUser ] = useAtom(userAtom)
    const [bookings, setBookings] = useState([] as Booking[]);
    const [snackbarState, setSnackbarState] = useState({
        openSnackbar: false,
        snackbarMessage: '',
        snackbarAlertColor: undefined as AlertColor | undefined
    });
    const { openSnackbar, snackbarMessage, snackbarAlertColor } = snackbarState;
    const navigate = useNavigate();
    const removeBookingMutation = useRemoveBookingMutation();

    useEffect(() => {
        if (!user) return;
        // Remove all previous bookings and sort it as well
        // Need to subtract one day in order to get the current day included in the array
        const now = dayjs().subtract(1, 'day');
        setBookings(user.bookings.filter(booking => now.diff(booking.date) < 0)
        .sort((a,b) => new Date(a.date).getDate() - new Date(b.date).getDate()));
    }, [user]);

    const Alert = forwardRef<HTMLDivElement, AlertProps>(function Alert(
        props,
        ref,
    ) {
        return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
    });

    const handleClose = (event?: React.SyntheticEvent | Event) => {
        setSnackbarState({
            ...snackbarState,
            openSnackbar: false,
        });
    }

    const handleUnbookFromChild = (bookingData: TUnbook) => {
        removeBookingMutation.mutate(bookingData, {
            onSuccess() {
                if (!user) return;
                setBookings(bookings.filter(booking => booking.id !== bookingData.id));
                getMe().then(res => {
                    setUser(res as User);
                    window.sessionStorage.setItem('user', JSON.stringify(res));
                });
                setSnackbarState({
                    snackbarMessage: 'Bookingen er nå fjernet',
                    openSnackbar: true,
                    snackbarAlertColor: 'success'
                });
            },
            onError() {
                setSnackbarState({
                    snackbarMessage: 'Kunne ikke fjerne bookingen',
                    openSnackbar: true,
                    snackbarAlertColor: 'error'
                });
            },
        });
    }

    return (
        <div className="w-full h-full flex flex-col overflow-hidden">
            <div className="p-2 flex flex-row align-baseline hover:cursor-pointer" onClick={() => navigate(`/home`)}>
                <ArrowCircleLeftIcon
                    htmlColor='#DF8B0D'
                />
                <p>Tilbake</p>
            </div>
            <div className="flex align-baseline justify-center p-2">
                <h1 className="text-2xl">Bookingoversikt</h1>
            </div>
            {user ? (
                <div className="h-full w-full overflow-auto p-2 flex flex-col gap-y-6">
                    {bookings.map((booking, i) =>
                        <BookingsListItem
                            key={i}
                            booking={booking}
                            user={user}
                            onUnbook={handleUnbookFromChild}
                        />
                    )}
                </div>
            ) : (
                <>Vi fant ingen registrerte bookings for deg</>
            )}

            <Snackbar open={openSnackbar} autoHideDuration={6000} onClose={handleClose} anchorOrigin={{ vertical: 'bottom', horizontal: 'center' }}>
                <Alert onClose={handleClose} severity={snackbarAlertColor} sx={{ width: '100%' }}>
                    {snackbarMessage}
                </Alert>
            </Snackbar>
        </div>
    )
}