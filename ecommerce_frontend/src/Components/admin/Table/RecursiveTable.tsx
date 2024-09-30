import React, { useState } from 'react';

interface CategoryGet {
    categoryId: number;
    name?: string;
    status?: boolean;
    subCategories?: CategoryGet[];
}

interface Props {
    configs: any[];
    data: any[];
}

const RecursiveTable = ({ configs, data }: Props) => {
    const [openCategories, setOpenCategories] = useState<number[]>([]);

    // Hàm để kiểm tra xem một danh mục có đang được mở hay không
    const isCategoryOpen = (categoryId: number) => {
        return openCategories.includes(categoryId);
    };

    // Hàm để xử lý việc mở/đóng một danh mục cha
    const toggleCategory = (categoryId: number) => {
        if (isCategoryOpen(categoryId)) {
            setOpenCategories(openCategories.filter(id => id !== categoryId));
        } else {
            setOpenCategories([...openCategories, categoryId]);
        }
    };

    // Hàm đệ quy render các hàng
    const renderRows = (configs: any[], data: any[], level: number = 0): React.ReactNode => {
        return data.map((itemData: CategoryGet, index) => {
            const key = itemData.categoryId;

            return (
                <React.Fragment key={index}>
                    <tr>
                        {configs.map((config: any, index: number) => (
                            // Thêm padding dựa trên level
                            <td key={index} style={{ paddingLeft: `${level * 20}px` }}>
                                {/* Chỉ hiển thị icon nếu danh mục có subCategories */}
                                {index === 0 && itemData.subCategories && itemData.subCategories.length > 0 ? (
                                    <span style={{ cursor: 'pointer' }} onClick={() => toggleCategory(key)}>
                                        {isCategoryOpen(key) ? '▼ ' : '► '}
                                        {config.render(itemData)}
                                    </span>
                                ) : (
                                    config.render(itemData)
                                )}
                            </td>
                        ))}
                    </tr>

                    {/* Nếu có subCategories và danh mục này đang mở, render đệ quy */}
                    {itemData.subCategories && isCategoryOpen(key) && renderRows(configs, itemData.subCategories, level + 1)}
                </React.Fragment>
            );
        });
    };

    return (
        <div className="col-12">
            <div className="rounded h-100 p-4">
                <h6 className="mb-4">Category List</h6>
                <div className="table-responsive">
                    <table className="table">
                        <thead>
                            <tr>
                                {configs.map((item: any, index: number) => (
                                    <th scope="col" key={index}>{item.label}</th>
                                ))}
                            </tr>
                        </thead>
                        <tbody>
                            {renderRows(configs, data)} {/* Gọi hàm renderRows */}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    );
};

export default RecursiveTable;
