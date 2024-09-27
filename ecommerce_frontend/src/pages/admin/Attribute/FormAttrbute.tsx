import { yupResolver } from '@hookform/resolvers/yup';
import React from 'react';
import { useForm, useFieldArray } from 'react-hook-form';
import * as yup from 'yup';
import { IoIosAdd } from "react-icons/io";
import { FiTrash } from "react-icons/fi";

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
  value1: string;
};

// Định nghĩa schema validation bằng Yup
const validationSchema = yup.object().shape({
  name: yup.string().required('Name is required').max(255, 'Name is too long'),
  values: yup
    .array()
    .required()
    .of(
      yup.object().shape({
        value1: yup.string().required('Value is required'),
      })
    )
    .min(1, 'At least one value is required'), // Bắt buộc ít nhất 1 giá trị
});

const FormAttribute = ({ handleAttribute, attribute }: Props) => {
  const {
    register,
    handleSubmit,
    control,
    formState: { errors },
  } = useForm<AttributeFormInput>({
    resolver: yupResolver(validationSchema),
    defaultValues: attribute || { name: '', values: [{ value1: '' }] }, // Đảm bảo có mảng `values` ngay từ đầu
  });
  // Sử dụng useFieldArray để quản lý mảng các giá trị
  const { fields, append, remove } = useFieldArray({
    control,
    name: 'values', // Tên trường mảng trong form
  });

  // Xử lý submit form
  const onSubmit = (data: AttributeFormInput) => {
    handleAttribute(data);
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      {/* Name Input */}
      <hr />
      <h6 className='mb-4' >Attribute</h6>
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
      <div className="mb-3">
        <h6 className='mb-4' >Values</h6>
        {fields.map((field, index) => (
          <div key={field.id} className="input-group mb-2">
            <input
              type="text"
              className={`form-control ${errors.values?.[index]?.value1 ? 'is-invalid' : ''}`}
              placeholder={`Value ${index + 1}`}
              {...register(`values.${index}.value1` as const)} // Đăng ký trường giá trị trong mảng
            />
            <button
              type="button"
              className="btn btn-danger"
              onClick={() => remove(index)} // Xóa giá trị
            >
            <FiTrash /> 
            </button>
            {errors.values?.[index]?.value1 && (
              <div className="invalid-feedback">{errors.values[index]?.value1?.message}</div>
            )}
          </div>
        ))}

        <div className='d-flex' >
          <button
            type="button"
            className="btn btn-success  me-2"
            onClick={() => append({ value1: '' })} // Thêm một giá trị mới
          >
            <IoIosAdd />
          </button>

          {/* Submit Button */}
          <button type="submit" className="btn btn-primary">
            Submit
          </button>
        </div>
      </div>
    </form>
  );
};

export default FormAttribute;
