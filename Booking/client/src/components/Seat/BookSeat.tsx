import { Seat } from "@type/seat";
import Chair from "@assets/chair.png";
import {
  Box,
  Button,
  CircularProgress,
  Modal,
  Typography,
} from "@mui/material";
import { useEffect, useState } from "react";
import { CreateBooking, useBook } from "@api/putBookingBook";
import dayjs, { Dayjs } from "dayjs";
import { Booking } from "@type/booking";
import { DeleteBooking, useUnbook } from "@api/putBookingUnbook";
import { useUser } from "@api/getUser";
import { useUserContext } from "@components/Provider/UserContextProvider";
import { isSameDay } from "date-fns";
import { User } from "@type/user";

const modalStyle = {
  position: "absolute" as "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: "60vw",
  bgcolor: "background.paper",
  border: "2px solid #000",
  borderRadius: 5,
  boxShadow: 24,
  p: 4,
};

const boxStyle = {
  border: "solid",
  borderRadius: "5px",
  padding: "30px 20px",
};

export const BookSeat = ({
  seat,
  date,
  booking,
}: {
  seat: Seat | undefined;
  date: Dayjs | null;
  booking: Booking | undefined;
}) => {

  // logged in user
  const loggedInUser = useUserContext().user;
  const loggedInUserAddBooking = (b: any) => useUserContext().addBooking(b);
  const loggedInUserRemoveBooking = (b: any) => useUserContext().removeBooking(b);

  // user occupying seat
  const [userId, setUserId] = useState("");
  const { isLoading, data, error } = useUser(userId);
  const [open, setOpen] = useState(false);
  const bookMutation = useBook();
  const unbookMutation = useUnbook();

  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  useEffect(() => {
    if (booking) {
      setUserId(booking?.userId.toString());
    }
  }, [date, data, booking, loggedInUser.bookings]);

  if (isLoading) {
    return (
      <div style={{ display: "flex" }}>
        <CircularProgress size={100} style={{ margin: "10vh auto" }} />
      </div>
    );
  }

  if (error) {
    return <h4>Kan ikke finne seteinformasjon.</h4>;
  }

  const handleBook = (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();
    const bookingData = {
      seatId: seat?.id,
      userId: loggedInUser.id,
      date: date?.toISOString(), //"2023-01-06T12:00:00.000+01",
    } as CreateBooking;
    bookMutation.mutate(bookingData);
    loggedInUserAddBooking(bookingData);
    
    // TODO: fiks så den bare kjører når respons har kode 204
    // if (mutation.isSuccess) handleOpen();
    handleOpen();
  };

  const handleUnbook = (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();
    const unbookingData = {
      userId: loggedInUser.id,
      date: date?.toISOString(), //"2023-01-06T12:00:00.000+01",
    } as DeleteBooking;
    unbookMutation.mutate(unbookingData);
    loggedInUserRemoveBooking(unbookingData);
    // TODO: fiks så den bare kjører når respons har kode 204
    // if (mutation.isSuccess) handleOpen();
    handleOpen();
  };

  const renderSeatInfoOccupied = () => {
    return (
      <div style={{ margin: "10px", textAlign: "center" }}>
        <Box style={boxStyle}>
          <img src={Chair} height="120" style={{ margin: "auto auto 20px" }} />
          <h3>Setenummer: {seat?.name}</h3>
          <h3>Romnummer: {seat?.roomId}</h3>
          <h3>Dato: {date?.toDate().toLocaleDateString()}</h3>
          <h3>Booket av: </h3>
          <div style={{ textAlign: "left" }}>
            <h3>Bruker: {data?.name}</h3>
            <h3>Telefon: {data?.phoneNumber}</h3>
            <h3>Email: {data?.email}</h3>
          </div>
        </Box>
      </div>
    );
  };

  const renderSeatInfoUnbook = () => {
    return (
      <div style={{ margin: "10px", textAlign: "center" }}>
        <Box style={boxStyle}>
          <img src={Chair} height="120" style={{ margin: "auto auto 20px" }} />
          <h3>Setenummer: {seat?.name}</h3>
          <h3>Romnummer: {seat?.roomId}</h3>
          <h3>Dato: {date?.toDate().toLocaleDateString()}</h3>
          <Button
            variant="contained"
            style={{ backgroundColor: "#DF8B0D" }}
            onClick={handleUnbook}
          >
            Fjern booking
          </Button>
          {
            <Modal
              open={open}
              onClose={handleClose}
              aria-labelledby="modal-modal-title"
              aria-describedby="modal-modal-description"
            >
              <Box sx={modalStyle}>
                <Typography id="modal-modal-title" variant="h6" component="h2">
                  Booking er fjernet!
                </Typography>
              </Box>
            </Modal>
          }
        </Box>
      </div>
    );
  };

  const renderSeatInfoBook = () => {
    // BUG: need to make sure loggedInUser.bookings got updated when booking/unbooking another seat that same day
    const userAlreadyBooked = loggedInUser.bookings.find(b => isSameDay(new Date(b.date), new Date(date!.toISOString())));
    if (userAlreadyBooked) {
      return renderSeatInfoDisabled()
    }
    else {
      return renderSeatInfoAvailable()
    }
  };

  const renderSeatInfoAvailable = () => {
    return (
      <div style={{ margin: "10px", textAlign: "center" }}>
      <Box style={boxStyle}>
        <img src={Chair} height="120" style={{ margin: "auto auto 20px" }} />
        <h3>Setenummer: {seat?.name}</h3>
        <h3>Romnummer: {seat?.roomId}</h3>
        <h3>Dato: {date?.toDate().toLocaleDateString()}</h3>
        <Button
          variant="contained"
          onClick={handleBook}
        >
          Book sete
        </Button>
        {
          <Modal
            open={open}
            onClose={handleClose}
            aria-labelledby="modal-modal-title"
            aria-describedby="modal-modal-description"
          >
            <Box sx={modalStyle}>
              <Typography id="modal-modal-title" variant="h6" component="h2">
                Setet er booket!
              </Typography>
            </Box>
          </Modal>
        }
      </Box>
    </div>
    )
  }

  const renderSeatInfoDisabled = () => {
    return (
      <div style={{ margin: "10px", textAlign: "center" }}>
        <Box style={boxStyle}>
          <img src={Chair} height="120" style={{ margin: "auto auto 20px" }} />
          <h3>Setenummer: {seat?.name}</h3>
          <h3>Romnummer: {seat?.roomId}</h3>
          <h3>Dato: {date?.toDate().toLocaleDateString()}</h3>
          <Button
            variant="contained"
            disabled={true}
          >
            Book sete
          </Button>
          <h5>Du har allerede booket for denne dagen.</h5>
        </Box>
      </div>
    );
  }

  if (booking && booking.userId != loggedInUser.id) {
    return <>{renderSeatInfoOccupied()}</>;
  }
  else if (booking && booking.userId == loggedInUser.id) {
    return <>{renderSeatInfoUnbook()}</>;
  } 
  else {
    return <>{renderSeatInfoBook()}</>;
  }
};
