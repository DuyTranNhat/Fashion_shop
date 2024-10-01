import React from 'react';
import FormBanner from './FormBanner';
import { toast } from 'react-toastify';

const InputBanner: React.FC = () => {

  return (
    <div className="bg-light rounded h-100 p-4">
      <h6 className="mb-4">Create a New Slide for Variant</h6>
      <FormBanner />
    </div>
  );
};

export default InputBanner;
