import React from 'react';

// TODO - add icons
const icons = {
  info: '',
  success: '',
  warning: '',
  error: '',
};

export type NotificationProps = {
  notification: {
    id: string;
    type: keyof typeof icons;
    title: string;
    message?: string;
  };
  onDismiss: (id: string) => void;
};

// add MUI Transition
// add MUI Button
const Notification = ({
  notification: { id, type, title, message },
  onDismiss,
}: NotificationProps) => {
  return (
    <>
      <div>
        <button
          onClick={() => {
            onDismiss(id);
          }}>
          <span className='sr-only'>Close</span>
          <p>X</p>
        </button>
      </div>
    </>
  );
};

export default Notification;
