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
import dayjs from 'dayjs';
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
  userData,
  onSeatInfoChange,
}: TDateSeat) => {
  const [date, setDate] = useAtom(dateAtom);
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

  const handleOccupiedDays = (
    day: Date,
    selectedDate: Date[],
    pickerDayProps: PickersDayProps<Date>
  ) => {
    const dayDate = dayjs(day);
    const isOccupied = occupiedDays?.find(day => day?.date === dayDate.date());
    const isOccupiedByCurrentUser = occupiedDays?.find(day => day?.date === dayDate.date() && day.userId === userData.id);

    const deskBookedByOtherUserStyle = {
      border: "2px solid #3981F1",
      background: "rgba(57, 129, 241, 0.25)"
    } as const

    const deskBookedByCurrentUserStyle = {
      border: "2px solid #61C577",
      background: "rgba(97, 197, 119, 0.25)"
    } as const
    return (
      <PickersDay
        sx={{
          ...(isOccupied && !isOccupiedByCurrentUser && deskBookedByOtherUserStyle),
          ...(isOccupiedByCurrentUser && deskBookedByCurrentUserStyle)
        }}
        {...pickerDayProps}
      />
    );
  };

  const handleMonthChange = (date: Date) => {
    setDate(date);
  };

  const handleChange = (newValue: Date) => {
    const dayDate = dayjs(newValue);

    const correctedDate = new Date(newValue.toISOString());
    
    occupiedDays?.find(day => {
      if (day?.date === dayDate.date() && day.userId === userData.id) {
        onSeatInfoChange('removeBookedSeat');
        setDate(new Date(correctedDate));
      }
      if (day?.date !== dayDate.date() && day?.userId !== userData.id) {
        onSeatInfoChange('bookAvailableSeat');
        setDate(new Date(correctedDate));
      }
      if (day?.date === dayDate.date() && day?.userId !== userData.id) {
        onSeatInfoChange('bookedSeat');
        setDate(new Date(correctedDate));
      }
    });
  };

  // userID = your or Someone else
  // undefined = default
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
