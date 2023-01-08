import React, { useEffect, useState } from 'react';
import { CircularProgress, Button, TextField } from '@mui/material';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import {
  LocalizationProvider,
  PickersDay,
  PickersDayProps,
  StaticDatePicker,
  nbNO,
} from '@mui/x-date-pickers';
import dayjs, { Dayjs } from 'dayjs';
import { useAtom } from 'jotai';
import { dateAtom } from '../../jotaiProvider';
import { Seat } from '@type/seat';
import { Booking } from '@type/booking';

export const DateSeat = ({ data }: { data: Seat }) => {
  const [occupiedDays, setOccupiedDays] = useState<number[]>();
  const [date, setDate] = useAtom(dateAtom);

  //   useEffect(() => {
  //     if (data && date) {
  //       setOccupiedDays(getOccupiedDays(data.bookings, date));
  //     }
  //   }, [data, date]);

  //   const getOccupiedDays = (bookings: Booking[], date: Date): number[] => {
  //     const dayDate = dayjs(date);
  //     return bookings.map((booking: Booking) => {
  //       if (
  //         dayjs(booking.date).month() === dayDate.month() &&
  //         dayjs(booking.date).year() === dayDate.year()
  //       ) {
  //       }
  //     });

  //     // bookings.forEach((b: Booking) => {
  //     //   if (dayjs(b.date).month() == date.month() && dayjs(b.date).year() == date.year()) {
  //     //     occupiedDaysInMonth.push(dayjs(b.date).date());
  //     //   }
  //     // });
  //   };

  const handleOccupiedDays = (
    day: Date,
    selectedDate: Date[],
    pickerDayProps: PickersDayProps<Date>
  ) => {
    const dayDate = dayjs(day);
    const isOccupied =
      !pickerDayProps.outsideCurrentMonth &&
      occupiedDays?.find(day => day === dayDate.date());
    return (
      <PickersDay
        sx={isOccupied ? { border: 'solid #DF8B0D' } : null}
        {...pickerDayProps}
      />
    );
  };

  const handleMonthChange = (date: Date) => {
    setDate(date);
    //setOccupiedDays(getOccupiedDays(data!.bookings, date));
  };

  const handleChange = (newValue: Date) => {
    setDate(newValue);
  };

  return (
    <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale='nb'>
      <StaticDatePicker
        value={date}
        onChange={() => handleChange}
        onMonthChange={handleMonthChange}
        renderInput={params => <TextField {...params} />}
        renderDay={handleOccupiedDays}
        closeOnSelect={false}
        showToolbar={false}
        disablePast={true}
        displayStaticWrapperAs='desktop'
      />
    </LocalizationProvider>
  );
};
