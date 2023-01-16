import { SetStateAction, Dispatch, useState } from 'react';
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
import { User } from '@type/user';

type TDateSeat = {
  data: Seat;
  onDateChange: () => void;
  userData: User;
  onSeatInfoChange: Dispatch<SetStateAction<string>>;
};

export const DateSeat = ({
  data,
  onDateChange,
  userData,
  onSeatInfoChange,
}: TDateSeat) => {
  const [date, setDate] = useAtom(dateAtom);

  // type OccupiedDays = [
  //   {
  //     date: number;
  //     userId: number;
  //   },
  //   { empty: '' }
  // ];

  const getOccupiedDays = (bookings: Booking[], date: Date) => {
    const dateDay = dayjs(date);

    const occupiedDaysInMonth = bookings.map((booking: Booking) => {
      if (
        dayjs(booking.date).month() === dateDay.month() &&
        dayjs(booking.date).year() === dateDay.year()
      )
        return { date: dayjs(booking.date).date(), userId: booking.userId };
    });

    return occupiedDaysInMonth;
  };

  const occupiedDays = getOccupiedDays(data?.bookings, date);
  console.log(occupiedDays);

  const handleOccupiedDays = (
    day: Date,
    selectedDate: Date[],
    pickerDayProps: PickersDayProps<Date>
  ) => {
    const dayDate = dayjs(day);
    const isOccupied = occupiedDays?.find(day => day?.date === dayDate.date());
    return (
      <PickersDay
        sx={isOccupied ? { border: 'solid #DF8B0D' } : null}
        {...pickerDayProps}
      />
    );
  };

  const handleMonthChange = (date: Date) => {
    setDate(date);
  };

  const handleChange = (newValue: Date) => {
    const dayDate = dayjs(newValue);
    occupiedDays?.find(day => {
      if (day?.date === dayDate.date() && day.userId === userData.id) {
        onSeatInfoChange('removeBookedSeat');
        setDate(newValue);
      }
      if (day?.date !== dayDate.date() && day?.userId !== userData.id) {
        onSeatInfoChange('bookAvailableSeat');
        setDate(newValue);
      }
      if (day?.date === dayDate.date() && day?.userId !== userData.id) {
        onSeatInfoChange('bookedSeat');
        setDate(newValue);
      }
    });
  };

  // userID = your or Someone else
  // undefined = default

  const dayDate = dayjs(date);

  return (
    <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale='nb'>
      <StaticDatePicker
        value={date}
        onChange={date => handleChange(date!)}
        onMonthChange={handleMonthChange}
        renderInput={params => <TextField {...params} />}
        renderDay={handleOccupiedDays}
        closeOnSelect={false}
        showToolbar={false}
        disablePast={true}
        displayStaticWrapperAs='desktop'
        openTo='day'
      />
    </LocalizationProvider>
  );
};
