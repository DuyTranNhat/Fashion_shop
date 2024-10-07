import { yupResolver } from '@hookform/resolvers/yup';
import React from 'react';
import { useForm, useFieldArray } from 'react-hook-form';
import * as yup from 'yup';
import { IoIosAdd } from "react-icons/io";

// Định nghĩa props
type Props = {
  attribute?: AttributeFormInput | null;
  handleAttribute: (form: AttributeFormInput) => void;
};

// Định nghĩa các loại dữ liệu đầu vào
export type AttributeFormInput = {
  name: string;
  values: ValueFormInput[];
};

export type ValueFormInput = {
  valueId?: number;
  value1: string;
  status: boolean;
};

// Định nghĩa schema validation bằng Yup
// Định nghĩa schema validation bằng Yup
const validationSchema = yup.object().shape({
  name: yup.string().required('Name is required').max(255, 'Name is too long'),
  values: yup
    .array()
    .of(
      yup.object().shape({
        value1: yup.string().required('Value is required'),
        valueId: yup.number(),
        status: yup.boolean().required(),
      })
    )
    .min(1, 'At least one value is required') // Bắt buộc ít nhất 1 giá trị
    .required(),
});


const FormAttribute = ({ handleAttribute, attribute }: Props) => {
  const {
    register,
    handleSubmit,
    control,
    setValue,
    formState: { errors },
  } = useForm<AttributeFormInput>({
    resolver: yupResolver(validationSchema),
    defaultValues: attribute || { name: '', values: [{ value1: '', valueId: 0, status: true }] }, // Đảm bảo có mảng `values` ngay từ đầu
  });

  // Sử dụng useFieldArray để quản lý mảng các giá trị
  const { fields, append } = useFieldArray({
    control,
    name: 'values', // Tên trường mảng trong form
  });

  // Xử lý submit form
  const onSubmit = (data: AttributeFormInput) => {
    handleAttribute(data);
  };

  // Hàm xử lý cập nhật status khi người dùng thay đổi checkbox
  const handleStatusChange = (index: number, checked: boolean) => {
    setValue(`values.${index}.status`, checked);
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      {/* Name Input */}
      <hr />
      <h6 className='mb-4'>Attribute</h6>
      <div className="form-floating mb-3">
        <input
          type="text"
          className={`form-control ${errors.name ? 'is-invalid' : ''}`}
          placeholder="Attribute Name"
          {...register('name')}
        />
        <label htmlFor="name">Attribute Name</label>
        {errors.name && <div className="invalid-feedback">{errors.name.message}</div>}
      </div>

      {/* Value Array Inputs */}
      { 
        
      }
      <div className="mb-3">
        <h6 className='mb-4'>Values</h6>
        {fields.map((field, index) => (
          <div key={field.id} className="input-group mb-2">
            <input
              type="text"
              className={`form-control ${errors.values?.[index]?.value1 ? 'is-invalid' : ''}`}
              placeholder={`Value ${index + 1}`}
              {...register(`values.${index}.value1` as const)} // Đăng ký trường giá trị trong mảng
            />
            {/* Checkbox để thay đổi trạng thái status */}
            <div className="form-check form-switch ms-2">
              <input
                className="form-check-input"
                type="checkbox"
                id={`statusCheck-${index}`}
                defaultChecked={field.status}
                onChange={(e) => handleStatusChange(index, e.target.checked)}
              />
              <label className="form-check-label" htmlFor={`statusCheck-${index}`}></label>
            </div>
            {errors.values?.[index]?.value1 && (
              <div className="invalid-feedback">{errors.values[index]?.value1?.message}</div>
            )}
          </div>
        ))}

        <div className='d-flex'>
          <button
            type="button"
            className="btn btn-success me-2"
            onClick={() => append({ value1: '', valueId: 0, status: true })} // Thêm một giá trị mới
          >
            <IoIosAdd />
          </button>

          {/* Submit Button */}
          <button type="submit" className="admin-btn-primary ">
            Submit
          </button>
        </div>
      </div>
    </form>
  );
};

export default FormAttribute;
