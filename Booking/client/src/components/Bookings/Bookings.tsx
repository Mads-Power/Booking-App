import { Alert, AlertProps, CircularProgress, Snackbar } from "@mui/material";
import { forwardRef, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useUserQuery } from "../../api/useUserQuery";
import { BookingsListItem } from "./components/BookingsListItem";
import ArrowCircleLeftIcon from '@mui/icons-material/ArrowCircleLeft';
import dayjs from 'dayjs';
import MuiAlert from '@mui/material/Alert';

import relativeTime from 'dayjs/plugin/relativeTime';
import { Booking } from "@type/booking";
import { useRemoveBookingMutation } from "@api/useRemoveBookingMutation";
import { AlertColor } from '@mui/material/Alert';


dayjs.extend(relativeTime)

export const Bookings = () => {
    const { userId } = useParams();
    const { isLoading, data, error } = useUserQuery(userId!);
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
        if (!data) return;
        // Remove all previous bookings
        const now = dayjs();
        setBookings(data.bookings.filter(booking => now.diff(booking.date) < 0));
    }, [data]);

    if (isLoading) {
        return <CircularProgress size={100} />;
    }

    if (error) {
        return <h1>Kunne ikke hente bruker</h1>;
    }

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

    const handleUnbookFromChild = (bookingData: Booking) => {
        removeBookingMutation.mutate(bookingData, {
            onSuccess() {
                if (!data) return;
                setBookings(bookings.filter(booking => booking.id !== bookingData.id));
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
            <div className="p-2 flex flex-row align-baseline hover:cursor-pointer" onClick={() => navigate(`/`)}>
                <ArrowCircleLeftIcon
                    htmlColor='#DF8B0D'
                />
                <p>Tilbake</p>
            </div>
            <div className="flex align-baseline justify-center p-2">
                <h1 className="text-2xl">Bookingoversikt</h1>
            </div>
            {data ? (
                <div className="h-full w-full overflow-auto p-2 flex flex-col gap-y-6">
                    {bookings.map((booking, i) =>
                        <BookingsListItem key={i} booking={booking} user={data} onUnbook={() => handleUnbookFromChild(booking)}></BookingsListItem>
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