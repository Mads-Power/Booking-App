import React, { useEffect, useState } from 'react';
import { useSeat } from '@api/getSeat';
import { CreateBooking, useBook } from '@api/putBookingBook';
import {
  CircularProgress,
  Button,
  TextField,
  Modal,
  Box,
  Typography,
} from '@mui/material';
import { useParams } from 'react-router-dom';
import Chair from '@assets/chair.png';
import { Booking } from '@type/booking';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import {
  LocalizationProvider,
  PickersDay,
  PickersDayProps,
  StaticDatePicker,
  nbNO,
} from '@mui/x-date-pickers';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import dayjs, { Dayjs } from 'dayjs';
import 'dayjs/locale/nb';
import { nbNO as coreNbNO } from '@mui/material/locale';
import styles from './Seat.module.css';

const modalStyle = {
  position: 'absolute' as 'absolute',
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  width: '60vw',
  bgcolor: 'background.paper',
  border: '2px solid #000',
  borderRadius: 5,
  boxShadow: 24,
  p: 4,
};

const theme = createTheme(
  {
    palette: {
      primary: { main: '#DF8B0D' },
    },
  },
  nbNO, // x-date-pickers translations
  coreNbNO // core translations
);

const getOccupiedDays = (bookings: Booking[], date: Dayjs): number[] => {
  let occupiedDaysInMonth: number[] = [];
  bookings.forEach((b: Booking) => {
    if (dayjs(b.date).month() == date.month()) {
      occupiedDaysInMonth.push(dayjs(b.date).date());
    }
  });
  return occupiedDaysInMonth;
};

export const Seat = () => {
  const { seatId } = useParams();
  const { isLoading, data, error } = useSeat(seatId!);
  const [date, setDate] = useState<Dayjs | null>(dayjs());
  const [occupiedDays, setOccupiedDays] = useState<number[]>();
  const [open, setOpen] = useState(false);
  const mutation = useBook();

  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  useEffect(() => {
    if (data && date) {
      setOccupiedDays(getOccupiedDays(data.bookings, date));
    }
  }, [data]);

  if (isLoading) {
    return (
      <div style={{ display: 'flex' }}>
        <CircularProgress size={100} style={{ margin: '10vh auto' }} />;
      </div>
    );
  }

  if (error) {
    return <h4>No Rooms Found</h4>;
  }

  const renderOccupiedDays = (
    day: Dayjs,
    _date: Dayjs[],
    DayComponentProps: PickersDayProps<Dayjs>
  ) => {
    const isOccupied =
      !DayComponentProps.outsideCurrentMonth && occupiedDays?.find(d => d == day.date());
    return (
      <PickersDay
        sx={
          isOccupied
            ? {
                border: 'solid #DF8B0D',
              }
            : undefined
        }
        {...DayComponentProps}
      />
    );
  };

  const handleMonthChange = (date: Dayjs) => {
    setOccupiedDays(getOccupiedDays(data!.bookings, date));
  };

  const handleBook = (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();
    const bookingData = {
      seatId: data?.id,
      userId: 1,
      date: date?.toISOString(), //"2023-01-06T12:00:00.000+01",
    } as CreateBooking;
    mutation.mutate(bookingData);

    // TODO: fiks så den bare kjører når respons har kode 204
    // if (mutation.isSuccess) handleOpen();
    handleOpen();
  };

  return (
    <>
      <ThemeProvider theme={theme}>
        <div
          style={{
            display: 'flex',
            flexDirection: 'column',
            overflow: 'hidden',
          }}>
          <div style={{ margin: '50px auto', textAlign: 'center' }}>
            <img src={Chair} height='120' style={{ margin: 'auto auto 20px' }} />
            <h3>Setenummer: {data?.name}</h3>
            <h3>Romnummer: {data?.roomId}</h3>
            <h3>Bruker: Test User</h3>
            <Button
              variant='contained'
              style={{ backgroundColor: '#DF8B0D' }}
              onClick={handleBook}>
              Book sete
            </Button>
            {
              <Modal
                open={open}
                onClose={handleClose}
                aria-labelledby='modal-modal-title'
                aria-describedby='modal-modal-description'>
                <Box sx={modalStyle}>
                  <Typography id='modal-modal-title' variant='h6' component='h2'>
                    Sete er booket!
                  </Typography>
                </Box>
              </Modal>
            }
          </div>
        </div>
        <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale='nb'>
          <StaticDatePicker
            value={date}
            onChange={newValue => {
              setDate(newValue);
              console.log(newValue?.toISOString());
            }}
            onMonthChange={handleMonthChange}
            renderInput={params => <TextField {...params} />}
            renderDay={renderOccupiedDays}
            closeOnSelect={false}
            showToolbar={false}
            disablePast={true}
            displayStaticWrapperAs='desktop'
            // shouldDisableYear={(year) => {
            //   if (year.year() > dayjs().year() + 1) return true;
            //   else return false;
            // }}
          />
        </LocalizationProvider>
      </ThemeProvider>
      {/* <div style={{display: "flex", flexDirection: "column", overflow: "hidden"}}>
          <div style={{margin: "0 auto"}}>
            {
              data?.bookings.map((b: any) => (
                <div key={b.id}>Date: {b.date}</div>
              ))
            }
          </div>
        </div> */}
    </>
  );
};
