import { useEffect, useState } from 'react';
import { useSeat } from '@api/getSeat';
import { CircularProgress, Button, TextField } from '@mui/material';
import { useParams, useNavigate } from 'react-router-dom';
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
import { BookSeat } from './BookSeat';
import {
  DateContextType,
  useDateContext,
} from '@components/Provider/DateContextProvider';
import { DateSeat } from './DateSeat';
import ArrowCircleLeftIcon from '@mui/icons-material/ArrowCircleLeft';
import { useAtom } from 'jotai';
import { dateAtom } from '../../jotaiProvider';

const theme = createTheme(
  {
    palette: {
      primary: { main: '#DF8B0D' },
    },
  },
  nbNO, // x-date-pickers translations
  coreNbNO // core translations
);

// const getOccupiedDays = (bookings: Booking[], date: Date): number[] => {
//   const dayDate = dayjs(date)
//   return bookings.map((booking: Booking) => {
//     if(dayjs(booking.date).month() === dayDate.month() && dayjs(booking.date).year() === dayDate.year()){

//     }
//   });

//   // bookings.forEach((b: Booking) => {
//   //   if (dayjs(b.date).month() == date.month() && dayjs(b.date).year() == date.year()) {
//   //     occupiedDaysInMonth.push(dayjs(b.date).date());
//   //   }
//   // });

// };

// const handleInitialDate = (date: Date) => {
//   if (date) {
//     return dayjs(date);
//   } else {
//     return dayjs();
//   }
// };

export const Seat = () => {
  const { seatId } = useParams();
  const { isLoading, data, error } = useSeat(seatId as string);
  const [date] = useAtom(dateAtom);
  const navigate = useNavigate();

  // Two date states are needed to handle convertion between Date type and Dayjs type
  //const { selectedDate, setSelectedDate }: DateContextType = useDateContext();
  //const [date, setDate] = useState<Dayjs | null>(handleInitialDate(selectedDate));
  // const [occupiedDays, setOccupiedDays] = useState<number[]>();

  // useEffect(() => {
  //   if (data && date) {
  //     setOccupiedDays(getOccupiedDays(data.bookings, date));
  //   }
  // }, [data, date]);

  if (isLoading) {
    return (
      <div style={{ display: 'flex' }}>
        <CircularProgress size={100} style={{ margin: '10vh auto' }} />
      </div>
    );
  }

  if (error) {
    return <h4>Kan ikke hente setet.</h4>;
  }

  const dayDate = dayjs(date);
  return (
    <>
      <ThemeProvider theme={theme}>
        <ArrowCircleLeftIcon htmlColor='#DF8B0D' onClick={() => navigate(`/`)} />
        <div
          style={{
            display: 'flex',
            flexDirection: 'column',
            overflow: 'hidden',
            margin: '10px',
            minWidth: '80vw',
          }}>
          <BookSeat
            seat={data!}
            date={dayDate}
            booking={
              data?.bookings.find(
                booking =>
                  dayjs(booking.date).format('YYYY-MM-DD') ===
                  dayDate?.format('YYYY-MM-DD')
              )!
            }
          />
        </div>
        <DateSeat data={data!} />
      </ThemeProvider>
    </>
  );
};
